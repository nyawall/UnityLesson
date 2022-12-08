using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class StateAttack : StateBase
{
    private enum AttackBehaviorTypes
    {
        Normal,
        OnAir,
        Dash,
    }
    private AttackBehaviorTypes _type;
    private Vector2 _normalAttackCastCenter = new Vector2(0.17f, 0.16f);
    private Vector2 _normalAttackCastSize = new Vector2(0.4f, 0.4f);
    private Vector2 _dashAttackCastCenter = new Vector2(0.18f, 0.16f);
    private Vector2 _dashAttackCastSize = new Vector2(0.7f, 0.4f);
    private LayerMask _targetLayer = 1<<LayerMask.NameToLayer("Enemy");
    private Player _player;

    public StateAttack(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
        _player = machine.GetComponent<Player>();
    }

    public override bool CanExecute()
    {
        return Machine.CurrentType == StateMachine.StateTypes.Idle ||
               Machine.CurrentType == StateMachine.StateTypes.Move ||
               Machine.CurrentType == StateMachine.StateTypes.Jump ||
               Machine.CurrentType == StateMachine.StateTypes.Fall ||
               Machine.CurrentType == StateMachine.StateTypes.Dash;
    }

    public override void Execute()
    {
        base.Execute();
        DoAttackBehavior();
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
                    MoveNext();
                }
                break;
            case Commands.Casting:
                {
                    if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
                    {
                        switch (_type)
                        {
                            case AttackBehaviorTypes.Normal:
                            case AttackBehaviorTypes.OnAir:
                                {
                                    RaycastHit2D hit = Physics2D.BoxCast((Vector2)Machine.transform.position
                                                                           + new Vector2(_normalAttackCastCenter.x * Movement.Direction, _normalAttackCastCenter.y),
                                                                          _normalAttackCastSize,
                                                                          0.0f,
                                                                          Vector2.zero,
                                                                          0.0f,
                                                                          _targetLayer);
                                    if (hit.collider)
                                    {
                                        hit.collider.GetComponent<Enemy>().Hurt(Machine.gameObject, _player.ATK, false);
                                    }
                                }
                                break;
                            case AttackBehaviorTypes.Dash:
                                {
                                    //RaycastHit2D[] hits = Physics2D.BoxCastAll((Vector2)Machine.transform.position
                                    //                                             + new Vector2(_normalAttackCastCenter.x * Movement.Direction, _normalAttackCastCenter.y),
                                    //                                            _normalAttackCastSize,
                                    //                                            0.0f,
                                    //                                            Vector2.zero,
                                    //                                            0.0f,
                                    //                                            _targetLayer);
                                    //
                                    //foreach (RaycastHit2D hit in hits)
                                    //{
                                    //    Enemy enemy = hit.collider.GetComponent<Enemy>();
                                    //    enemy.HP -= _player.ATK;
                                    //}


                                    IEnumerable<Enemy> enemies = Physics2D.BoxCastAll((Vector2)Machine.transform.position 
                                                                                        + new Vector2(_normalAttackCastCenter.x * Movement.Direction, _normalAttackCastCenter.y),
                                                                                       _normalAttackCastSize,
                                                                                       0.0f,
                                                                                       Vector2.zero,
                                                                                       0.0f,
                                                                                       _targetLayer)
                                                                            .Select(hit => hit.collider.gameObject.GetComponent<Enemy>());

                                    foreach (Enemy enemy in enemies)
                                    {
                                        enemy.Hurt(Machine.gameObject, _player.ATK, false);
                                    }
                                }
                                break;
                            default:
                                break;
                        }


                        MoveNext();
                    }
                }
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

    private void DoAttackBehavior()
    {
        Movement.DirectionChangable = false;
        Movement.Movable = false;
        if (Machine.PreviousType == StateMachine.StateTypes.Dash)
        {
            _type = AttackBehaviorTypes.Dash;
            Animator.Play("DashAttack");
        }
        else if (Machine.PreviousType == StateMachine.StateTypes.Jump ||
                 Machine.PreviousType == StateMachine.StateTypes.Fall)
        {
            _type = AttackBehaviorTypes.OnAir;
            Animator.Play("Attack");
        }
        else
        {
            _type = AttackBehaviorTypes.Normal;
            Movement.ResetMove();
            Animator.Play("Attack");
        }
    }
}
