using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHurt : StateBase
{
    public StateHurt(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
    }

    public override bool CanExecute()
    {
        return Machine.CurrentType != StateMachine.StateTypes.Attack ||
               Machine.CurrentType != StateMachine.StateTypes.Die;
    }

    public override void Execute()
    {
        base.Execute();
        Animator.Play("Hurt");
        Movement.DirectionChangable = false;
        Movement.Movable = false;
        Movement.ResetMove();
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
                {
                    if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
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
