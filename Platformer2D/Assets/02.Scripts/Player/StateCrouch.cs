using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCrouch : StateBase
{
    private Rigidbody2D _rb;
    private CapsuleCollider2D[] _cols;
    private Vector2 _offset = new Vector2(0.0f, 0.07f);
    private Vector2 _size = new Vector2(0.14f, 0.14f);
    private Vector2 _offsetOrigin;
    private Vector2 _sizeOrigin;

    public StateCrouch(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
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
        Current = Commands.OnAction;
        ShrinkColliders();
        Animator.Play("Crouch");
        Movement.DirectionChangable = true;
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
                break;
            case Commands.Casting:
                break;
            case Commands.OnAction:
                {
                    if (Input.GetKey(KeyCode.LeftAlt))
                        next = StateMachine.StateTypes.Jump;
                    else if (Input.GetKey(KeyCode.DownArrow) == false)
                        next = StateMachine.StateTypes.Idle;
                }
                break;
            case Commands.Finish:
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
