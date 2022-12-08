using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class Movement : MonoBehaviour
{
    public const int DIRECTION_LEFT = -1;
    public const int DIRECTION_RIGHT = 1;
    public bool DirectionChangable;
    private int _direction;
    public int Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            if (DirectionChangable == false)
                return;

            if (value < 0)
            {
                _direction = DIRECTION_LEFT;
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            }
            else if (value > 0)
            {
                _direction = DIRECTION_RIGHT;
                transform.eulerAngles = Vector3.zero;
            }
        }
    }

    public bool Movable;
    private float _h => Input.GetAxis("Horizontal");
    private float _tolerance = 0.05f;
    private Vector2 _move;
    [SerializeField] private float _speed = 2.0f;
    private Rigidbody2D _rb;
    private StateMachine _machine;

    public void RefreshMove()
    {
        _move.x = _h * _speed;
    }

    public void ResetMove()
    {
        _move = Vector2.zero;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _machine = GetComponent<StateMachine>();
        Direction = DIRECTION_RIGHT;
    }

    private void Update()
    {
        if (DirectionChangable)
        {
            if (_h > _tolerance)
                Direction = DIRECTION_RIGHT;
            else if (_h < -_tolerance)
                Direction = DIRECTION_LEFT;
        }

        if (Movable)
        {
            if (Mathf.Abs(_h) > _tolerance)
            {
                _machine.ChangeState(StateMachine.StateTypes.Move);
            }
            else
            {
                _machine.ChangeState(StateMachine.StateTypes.Idle);
            }
            
            _move.x = _h * _speed;
        }        
    }

    private void FixedUpdate()
    {
        //_rb.MovePosition(_rb.position + _move * Time.fixedDeltaTime);
        _rb.position += _move * Time.fixedDeltaTime;
    }
}
