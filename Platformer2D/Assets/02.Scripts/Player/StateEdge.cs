using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateEdge : StateBase
{
    private enum EdgeBehaviorTypes
    {
        EdgeGrab,
        EdgeIdle,
        EdgeClimb,
    }
    private EdgeBehaviorTypes _type;
    private EdgeDetector _edgeDetector;
    private Rigidbody2D _rb;
    private Movement _movement;

    private Vector2 _climbStartPos;
    private float _climbStartTimeMark;
    private float _climbSpeedGain = 2.5f;

    public StateEdge(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
        _edgeDetector = machine.GetComponentInChildren<EdgeDetector>();
        _rb = machine.GetComponent<Rigidbody2D>();
        _movement = machine.GetComponent<Movement>();
    }

    public override bool CanExecute()
    {
        return _edgeDetector.IsDetected &&
                (Machine.CurrentType == StateMachine.StateTypes.Jump ||
                 Machine.CurrentType == StateMachine.StateTypes.Fall);
    }

    public override void Execute()
    {
        base.Execute();        
        _type = EdgeBehaviorTypes.EdgeGrab;
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.velocity = Vector2.zero;
        _movement.ResetMove();
        _movement.DirectionChangable = false;
        _movement.Movable = false;
    }

    public override void Stop()
    {
        base.Stop();
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public override StateMachine.StateTypes Update()
    {
        StateMachine.StateTypes next = Type;

        switch (_type)
        {
            case EdgeBehaviorTypes.EdgeGrab:
                next = EdgeGrabWorkflow();
                break;
            case EdgeBehaviorTypes.EdgeIdle:
                next = EdgeIdleWorkflow();
                break;
            case EdgeBehaviorTypes.EdgeClimb:
                next = EdgeClimbWorkflow();
                break;
            default:
                break;
        }

        return next;
    }

    private void PlayAnimationClip()
    {
        switch (_type)
        {
            case EdgeBehaviorTypes.EdgeGrab:
                Animator.Play("EdgeGrab");
                break;
            case EdgeBehaviorTypes.EdgeIdle:
                Animator.Play("EdgeIdle");
                break;
            case EdgeBehaviorTypes.EdgeClimb:
                Animator.Play("EdgeClimb");
                break;
            default:
                break;
        }
    }

    private StateMachine.StateTypes EdgeGrabWorkflow()
    {
        StateMachine.StateTypes next = Type;

        switch (Current)
        {
            case Commands.Idle:
                break;
            case Commands.Prepare:
                {
                    PlayAnimationClip();
                    MoveNext();
                }
                break;
            case Commands.Casting:
                {
                    MoveNext();
                }
                break;
            case Commands.OnAction:
                {
                    if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        MoveNext();
                    }
                }
                break;
            case Commands.Finish:
                {
                    _type = EdgeBehaviorTypes.EdgeIdle;
                    Current = Commands.Prepare;
                }
                break;
            default:
                break;
        }

        return next;
    }

    private StateMachine.StateTypes EdgeIdleWorkflow()
    {
        StateMachine.StateTypes next = Type;

        switch (Current)
        {
            case Commands.Idle:
                break;
            case Commands.Prepare:
                {
                    PlayAnimationClip();
                    MoveNext();
                }
                break;
            case Commands.Casting:
                {
                    MoveNext();
                }
                break;
            case Commands.OnAction:
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        _type = EdgeBehaviorTypes.EdgeClimb;
                        Current = Commands.Prepare;
                    }

                    if (_edgeDetector.IsDetected == false)
                        next = StateMachine.StateTypes.Fall;
                }
                break;
            case Commands.Finish:
                break;
            default:
                break;
        }

        return next;
    }

    private StateMachine.StateTypes EdgeClimbWorkflow()
    {
        StateMachine.StateTypes next = Type;

        switch (Current)
        {
            case Commands.Idle:
                break;
            case Commands.Prepare:
                {
                    PlayAnimationClip();
                    _climbStartPos = _rb.position;
                    _climbStartTimeMark = Time.time;
                    MoveNext();
                }
                break;
            case Commands.Casting:
                {
                    MoveNext();
                }
                break;
            case Commands.OnAction:
                {
                    if ((Time.time - _climbStartTimeMark) * _climbSpeedGain > 1.0f)
                    {
                        MoveNext();
                    }
                    else
                    {
                        Debug.Log($"{_climbStartPos}, {_edgeDetector.DetectedPosition}");
                        _rb.position = Vector2.Lerp(_climbStartPos,
                                                    _edgeDetector.DetectedPosition,
                                                    (Time.time - _climbStartTimeMark) * _climbSpeedGain);
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
