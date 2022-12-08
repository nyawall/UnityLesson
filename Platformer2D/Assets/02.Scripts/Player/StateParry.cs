using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateParry : StateBase
{
    private ProjectileDetector _detector;
    public StateParry(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
        _detector = machine.GetComponentInChildren<ProjectileDetector>();
    }

    public override bool CanExecute()
    {
        return _detector.IsDetected &&
               (Machine.CurrentType == StateMachine.StateTypes.Idle ||
                Machine.CurrentType == StateMachine.StateTypes.Move ||
                Machine.CurrentType == StateMachine.StateTypes.Jump ||
                Machine.CurrentType == StateMachine.StateTypes.Fall ||
                Machine.CurrentType == StateMachine.StateTypes.Dash ||
                Machine.CurrentType == StateMachine.StateTypes.Slide);
    }

    public override void Execute()
    {
        base.Execute();
        Movement.DirectionChangable = false;
        Movement.Movable = false;
        Animator.Play("Parry");
        _detector.ProjectileDetected.GetComponent<Projectile>().SetUp(Machine.gameObject,
                                                                      -_detector.ProjectileDetectedVelocity,
                                                                      2.0f,
                                                                      LayerMask.NameToLayer("PlayerProjectile"),
                                                                      LayerMask.NameToLayer("Enemy"));
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
