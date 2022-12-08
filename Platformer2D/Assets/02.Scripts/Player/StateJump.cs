using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StateJump : StateBase
{
    public enum JumpTypes
    {
        Normal,
        Down
    }
    private JumpTypes _jumpType;
    private GroundDetector _groundDetector;
    private Rigidbody2D _rb;
    public StateJump(StateMachine.StateTypes type, StateMachine machine) : base(type, machine)
    {
        _groundDetector = machine.GetComponentInChildren<GroundDetector>();
        _rb = machine.GetComponent<Rigidbody2D>();
    }

    public override bool CanExecute()
    {
        return ((Machine.CurrentType == StateMachine.StateTypes.Idle ||
                Machine.CurrentType == StateMachine.StateTypes.Move ||
                Machine.CurrentType == StateMachine.StateTypes.Crouch) &&
                _groundDetector.IsDetected) ||
                Machine.CurrentType == StateMachine.StateTypes.LadderUp ||
                Machine.CurrentType == StateMachine.StateTypes.LadderDown;
    }

    public override void Execute()
    {
        base.Execute();
        DecideJumpType();
        Animator.Play("Jump");
        Movement.DirectionChangable = true;
        Movement.Movable = false;
        Movement.RefreshMove();
    }

    public override StateMachine.StateTypes Update()
    {
        switch (_jumpType)
        {
            case JumpTypes.Normal:
                return NormalJumpWorkflow();
            case JumpTypes.Down:
                return DownJumpWorkflow();
            default:
                throw new System.Exception("[StateJump] : ��ȿ���� ���� ���� Ÿ���Դϴ�.");
        }
    }

    private void DecideJumpType()
    {
        if (Machine.PreviousType == StateMachine.StateTypes.Crouch)
            _jumpType = JumpTypes.Down;
        else
            _jumpType = JumpTypes.Normal;
    }

    private StateMachine.StateTypes NormalJumpWorkflow()
    {
        StateMachine.StateTypes next = Type;

        switch (Current)
        {
            case Commands.Idle:
                break;
            // ���� Y �ӵ� 0 ����� �� �������� �� ���ϱ�
            //--------------------------------------
            case Commands.Prepare:
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, 0.0f);
                    _rb.AddForce(Vector2.up * 2.8f, ForceMode2D.Impulse);
                    MoveNext();
                }
                break;
            // ���������� ���� �Ǿ����� (���� �׶��忡�� ����������)
            //------------------------------------------------
            case Commands.Casting:
                {
                    if (_groundDetector.IsDetected == false)
                        MoveNext();
                }
                break;
            // Y �ӵ��� ������ �Ǵ¼��� ���� �׼� ����
            //------------------------------------------------
            case Commands.OnAction:
                {
                    if (_rb.velocity.y < 0)
                        MoveNext();
                }
                break;
            // ���� ��� �������� �������� Fall ���·� ��ȯ
            //--------------------------------------------------
            case Commands.Finish:
                {
                    next = StateMachine.StateTypes.Fall;
                }
                break;
            default:
                break;
        }

        return next;
    }

    private StateMachine.StateTypes DownJumpWorkflow()
    {
        StateMachine.StateTypes next = Type;

        switch (Current)
        {
            case Commands.Idle:
                break;
            // ���� Y �ӵ� 0 ����� �� �������� �� ���ϱ�
            //--------------------------------------
            case Commands.Prepare:
                {
                    // ���� �׶��� ���� �Ұ����ϴٸ� Idle�� ����
                    if (_groundDetector.IsUnderGroundExist() == false ||
                        _groundDetector.IgnoreCurrentGround() == false)
                    {
                        return StateMachine.StateTypes.Idle;
                    }

                    _rb.velocity = new Vector2(_rb.velocity.x, 0.0f);
                    _rb.AddForce(Vector2.up * 1.0f, ForceMode2D.Impulse);
                    MoveNext();
                }
                break;
            // Nothing to do
            //------------------------------------------------
            case Commands.Casting:
                {
                    MoveNext();
                }
                break;
            // Y �ӵ��� ������ �Ǵ¼��� ���� �׼� ����
            //------------------------------------------------
            case Commands.OnAction:
                {
                    if (_rb.velocity.y < 0)
                        MoveNext();
                }
                break;
            // ���� ��� �������� �������� Fall ���·� ��ȯ
            //--------------------------------------------------
            case Commands.Finish:
                {
                    next = StateMachine.StateTypes.Fall;
                }
                break;
            default:
                break;
        }

        return next;
    }
}
