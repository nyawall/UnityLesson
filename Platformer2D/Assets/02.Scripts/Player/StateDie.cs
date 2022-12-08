using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDie : StateBase
{
    public StateDie(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
    }

    public override bool CanExecute()
    {
        return true;
    }

    public override void Execute()
    {
        base.Execute();
        Animator.Play("Die");
        Movement.DirectionChangable = false;
        Movement.Movable = false;
        Movement.ResetMove();
    }

    public override StateMachine.StateTypes Update()
    {
        return Type;
    }
}
