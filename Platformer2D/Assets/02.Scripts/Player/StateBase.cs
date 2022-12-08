using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    // �� ���°� ������ ��� ����
    public enum Commands
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    protected Commands Current; // ���� �� ���°� �����ؾ��ϴ� ���
    protected StateMachine Machine; // �� ���¸� ������ ���
    protected StateMachine.StateTypes Type; // ��� ���忡�� �� ���¿� ���� Ÿ��
    protected Animator Animator;
    protected Movement Movement;

    public StateBase(StateMachine.StateTypes type, StateMachine machine)
    {
        Type = type;
        Machine = machine;
        Animator = machine.GetComponent<Animator>();
        Movement = machine.GetComponent<Movement>();
    }

    /// <summary>
    /// �� ���¸� ������ �� �ִ� ����
    /// </summary>
    public abstract bool CanExecute();

    /// <summary>
    /// �� ���� ����
    /// </summary>
    public virtual void Execute()
    {
        Current = Commands.Prepare;
    }

    /// <summary>
    /// �� ���� �ߴ�
    /// </summary>
    public virtual void Stop()
    {
        Current = Commands.Idle;
    }

    /// <summary>
    /// �� ���� ����� �����ϵ��� ��
    /// </summary>
    public void MoveNext()
    {
        if (Current < Commands.Finish)
            Current++;
    }

    /// <summary>
    /// �� ������ ���� ��ɿ� ���� ������ ������
    /// </summary>
    /// <returns> �ӽ��� ��ȯ�ؾ��ϴ� ���� ���� Ÿ�� </returns>
    public virtual StateMachine.StateTypes Update()
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
                next = default(StateMachine.StateTypes);
                break;
            default:
                break;
        }

        return next;
    }
}
