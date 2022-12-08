using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLadderDown : StateBase
{
    private LadderDetector _ladderDetector;
    private GroundDetector _groundDetector;
    private Rigidbody2D _rb;
    private Movement _movement;
    public StateLadderDown(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
        _ladderDetector = machine.GetComponentInChildren<LadderDetector>();
        _groundDetector = machine.GetComponentInChildren<GroundDetector>();
        _rb = machine.GetComponent<Rigidbody2D>();
        _movement = machine.GetComponent<Movement>();
    }

    public override bool CanExecute()
    {
        return _ladderDetector.IsGoDownPossible &&
               (Machine.CurrentType == StateMachine.StateTypes.Idle ||
                Machine.CurrentType == StateMachine.StateTypes.Move ||
                Machine.CurrentType == StateMachine.StateTypes.Slide ||
                Machine.CurrentType == StateMachine.StateTypes.Dash);
    }

    public override void Execute()
    {
        base.Execute();
        _rb.bodyType = RigidbodyType2D.Kinematic;
        Animator.Play("Ladder");
        _movement.DirectionChangable = false;
        _movement.Movable = false;
    }

    public override void Stop()
    {
        base.Stop();
        _rb.bodyType = RigidbodyType2D.Dynamic;
        Animator.speed = 1.0f;
    }

    public override StateMachine.StateTypes Update()
    {
        StateMachine.StateTypes next = Type;

        switch (Current)
        {
            case Commands.Idle:
                break;
            case Commands.Prepare:
                {
                    _rb.velocity = Vector2.zero;
                    _movement.ResetMove();

                    _rb.position = new Vector2(_ladderDetector.DownPosX, _ladderDetector.DownTopStartPosY);

                    Current = Commands.OnAction;
                }
                break;
            case Commands.Casting:
                break;
            case Commands.OnAction:
                {
                    // Vertical 축 입력
                    float v = Input.GetAxis("Vertical");
                    Animator.speed = Mathf.Abs(v);
                    _rb.position += Vector2.up * v * Time.deltaTime;

                    // 탈출 조건
                    if (_rb.position.y > _ladderDetector.DownTopEscapePosY)
                    {
                        _rb.position = new Vector2(_ladderDetector.DownPosX, _ladderDetector.DownLadderTopY);
                        MoveNext();
                    }
                    else if (_rb.position.y < _ladderDetector.DownBottomEscapePosY)
                    {
                        _rb.position = new Vector2(_ladderDetector.DownPosX, _ladderDetector.DownLadderBottomY);
                        MoveNext();
                    }
                    else if (_groundDetector.IsDetected)
                    {
                        MoveNext();
                    }
                }
                break;
            case Commands.Finish:
                {
                    next = StateMachine.StateTypes.Idle;
                }
                break;
            default:
                break;
        }

        return next;
    }
}
