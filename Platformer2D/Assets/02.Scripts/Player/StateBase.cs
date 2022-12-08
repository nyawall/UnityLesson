using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    // 이 상태가 수행할 명령 종류
    public enum Commands
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    protected Commands Current; // 현재 이 상태가 수행해야하는 명령
    protected StateMachine Machine; // 이 상태를 실행할 기계
    protected StateMachine.StateTypes Type; // 기계 입장에서 이 상태에 대한 타입
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
    /// 이 상태를 실행할 수 있는 조건
    /// </summary>
    public abstract bool CanExecute();

    /// <summary>
    /// 이 상태 실행
    /// </summary>
    public virtual void Execute()
    {
        Current = Commands.Prepare;
    }

    /// <summary>
    /// 이 상태 중단
    /// </summary>
    public virtual void Stop()
    {
        Current = Commands.Idle;
    }

    /// <summary>
    /// 그 다음 명령을 수행하도록 함
    /// </summary>
    public void MoveNext()
    {
        if (Current < Commands.Finish)
            Current++;
    }

    /// <summary>
    /// 이 상태의 현재 명령에 따른 동작을 수행함
    /// </summary>
    /// <returns> 머신이 전환해야하는 다음 상태 타입 </returns>
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
