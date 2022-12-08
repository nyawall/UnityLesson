using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFall : StateBase
{
    private GroundDetector _groundDetector;
    public StateFall(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
        _groundDetector = machine.GetComponentInChildren<GroundDetector>();
    }

    public override bool CanExecute()
    {
        return (Machine.CurrentType == StateMachine.StateTypes.Jump ||
                Machine.CurrentType == StateMachine.StateTypes.Idle ||
                Machine.CurrentType == StateMachine.StateTypes.Move) &&
                _groundDetector.IsDetected == false;
    }

    public override void Execute()
    {
        Current = Commands.OnAction;
        Animator.Play("Fall");
        Movement.DirectionChangable = true;
        Movement.Movable = false;
    }

    public override StateMachine.StateTypes Update()
    {
        StateMachine.StateTypes next = Type;

        switch (Current)
        {
            case Commands.Idle:
                break;
            case Commands.Prepare:
                break;
            case Commands.Casting:
                break;
            case Commands.OnAction:
                if (_groundDetector.IsDetected)
                {
                    MoveNext();
                }
                break;
            case Commands.Finish:
                next = StateMachine.StateTypes.Idle;
                break;
            default:
                break;
        }

        return next;
    }
}
