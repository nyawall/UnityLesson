using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSlide : StateBase
{
    private Rigidbody2D _rb;
    private CapsuleCollider2D[] _cols;
    private Vector2 _offset = new Vector2(0.0f, 0.07f);
    private Vector2 _size = new Vector2(0.14f, 0.14f);
    private Vector2 _offsetOrigin;
    private Vector2 _sizeOrigin;

    public StateSlide(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
        _rb = machine.GetComponent<Rigidbody2D>();
        _cols = machine.GetComponentsInChildren<CapsuleCollider2D>();
        _offsetOrigin = _cols[0].offset;
        _sizeOrigin = _cols[0].size;
    }

    public override bool CanExecute()
    {
        return Machine.CurrentType == StateMachine.StateTypes.Idle ||
               Machine.CurrentType == StateMachine.StateTypes.Move;
    }

    public override void Execute()
    {
        base.Execute();
        ShrinkColliders();
        Animator.Play("Slide");
        Movement.DirectionChangable = false;
        Movement.Movable = false;
        Movement.ResetMove();
    }

    public override void Stop()
    {
        base.Stop();
        RollBackColliders();
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

    private void ShrinkColliders()
    {
        for (int i = 0; i < _cols.Length; i++)
        {
            _cols[i].offset = _offset;
            _cols[i].size = _size;
        }
    }

    private void RollBackColliders()
    {
        for (int i = 0; i < _cols.Length; i++)
        {
            _cols[i].offset = _offsetOrigin;
            _cols[i].size = _sizeOrigin;
        }
    }
}
