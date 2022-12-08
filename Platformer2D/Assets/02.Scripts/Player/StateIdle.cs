using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : StateBase
{
    private GroundDetector _groundDetector;
    public StateIdle(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
        _groundDetector = machine.GetComponentInChildren<GroundDetector>();
    }

    public override bool CanExecute()
    {
        return true;
    }

    public override void Execute()
    {
        base.Execute();
        Animator.Play("Idle");
        Movement.DirectionChangable = true;
        Movement.Movable = true;
    }

    public override StateMachine.StateTypes Update()
    {
        StateMachine.StateTypes next = Type;

        switch (Current)
        {
            case Commands.Idle:
                break;
            case Commands.Prepare:
                MoveNext();
                break;
            case Commands.Casting:
                MoveNext();
                break;
            case Commands.OnAction:
                MoveNext();
                break;
            case Commands.Finish:
                {
                    if (_groundDetector.IsDetected == false)
                    {
                        next = StateMachine.StateTypes.Fall;
                    }
                }
                break;
            default:
                break;
        }

        return next;
    }
}

