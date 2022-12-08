using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDash : StateBase
{
    private Rigidbody2D _rb;
    public StateDash(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
        _rb = machine.GetComponent<Rigidbody2D>();
    }

    public override bool CanExecute()
    {
        return Machine.CurrentType == StateMachine.StateTypes.Idle ||
               Machine.CurrentType == StateMachine.StateTypes.Move ||
               Machine.CurrentType == StateMachine.StateTypes.Jump ||
               Machine.CurrentType == StateMachine.StateTypes.Fall;
    }

    public override void Execute()
    {
        base.Execute();
        Animator.Play("Dash");
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
                {
                    _rb.velocity = Vector2.zero;
                    _rb.AddForce(Vector2.right * Movement.Direction * 3.0f, ForceMode2D.Impulse);
                    Current = Commands.OnAction;
                }
                break;
            case Commands.Casting:
                break;
            case Commands.OnAction:
                {
                    if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        Current = Commands.Finish;
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
