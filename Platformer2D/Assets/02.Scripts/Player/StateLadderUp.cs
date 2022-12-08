using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLadderUp : StateBase
{
    private LadderDetector _ladderDetector;
    private GroundDetector _groundDetector;
    private Rigidbody2D _rb;
    private Movement _movement;
    public StateLadderUp(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
        _ladderDetector = machine.GetComponentInChildren<LadderDetector>();
        _groundDetector = machine.GetComponentInChildren<GroundDetector>();
        _rb = machine.GetComponent<Rigidbody2D>();
        _movement = machine.GetComponent<Movement>();
    }

    public override bool CanExecute()
    {
        return _ladderDetector.IsGoUpPossible &&
               (Machine.CurrentType == StateMachine.StateTypes.Idle ||
                Machine.CurrentType == StateMachine.StateTypes.Move ||
                Machine.CurrentType == StateMachine.StateTypes.Jump ||
                Machine.CurrentType == StateMachine.StateTypes.Fall);
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

                    // �ƿ� �ؿ��� �ö�����Ҷ�
                    if (_rb.position.y <= _ladderDetector.UpBottomStartPosY)
                    {
                        _rb.position = new Vector2(_ladderDetector.UpPosX, _ladderDetector.UpBottomStartPosY);
                    }
                    // �߰��� ����/������ ������ ���߿� ���ִ� ���¿��� ��ٸ��� �Ŵ޸���
                    else
                    {
                        _rb.position = new Vector2(_ladderDetector.UpPosX, _rb.position.y);
                    }

                    Current = Commands.OnAction;
                }
                break;
            case Commands.Casting:
                break;
            case Commands.OnAction:
                {
                    // Vertical �� �Է�
                    float v = Input.GetAxis("Vertical");
                    Animator.speed = Mathf.Abs(v);
                    _rb.position += Vector2.up * v * Time.deltaTime;

                    // Ż�� ����
                    if (_rb.position.y > _ladderDetector.UpTopEscapePosY)
                    {
                        _rb.position = new Vector2(_ladderDetector.UpPosX, _ladderDetector.UpLadderTopY);
                        MoveNext();
                    }
                    else if (_rb.position.y < _ladderDetector.UpBottomEscapePosY)
                    {
                        _rb.position = new Vector2(_ladderDetector.UpPosX, _ladderDetector.UpLadderBottomY);
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
