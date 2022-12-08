using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum StateTypes
    {
        Idle,
        Move,
        Jump,
        Fall,
        Attack,
        Dash,
        Crouch,
        Slide,
        LadderUp,
        LadderDown,
        Edge,
        Hurt,
        Die,
        Parry,
    }
    public StateTypes CurrentType;
    public StateTypes PreviousType;
    public StateBase Current;
    private Dictionary<StateTypes, StateBase> _states = new Dictionary<StateTypes, StateBase>();
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        InitStates();
    }

    private void Start()
    {
        Current = _states[default(StateTypes)];
        CurrentType = default(StateTypes);

        RegisterCallbacks();
        RegisterShortcuts();
    }

    /// <summary>
    /// 상태를 전환하는 함수
    /// </summary>
    /// <param name="newStateType"> 전환하려는 상태 타입 </param>
    public bool ChangeState(StateTypes newStateType)
    {
        // 바꾸려는 타입이 현재 수행중인 상태와 같은 타입이면 상태를 전환하지 않는다.
        if (CurrentType == newStateType)
            return false;

        // 바꾸려는 상태가 실행 가능하지 않다면 상태를 전환하지 않는다.
        if (_states[newStateType].CanExecute() == false)
            return false;

        Current.Stop(); // 현재 수행중인 상태 중단
        Current = _states[newStateType]; // 다른 상태로 전환
        PreviousType = CurrentType; // 이전 상태 기억
        CurrentType = newStateType; // 현재 상태 타입을 전환한 타입으로 갱신
        Current.Execute(); // 전환된 상태 실행
        return true;
    }

    private void Update()
    {
        ChangeState(Current.Update()); // 상태 수행 후 전환해야하는 타입으로 상태 전환
    }

    private void InitStates()
    {
        _states.Add(StateTypes.Idle, new StateIdle(StateTypes.Idle, this));
        _states.Add(StateTypes.Move, new StateMove(StateTypes.Move, this));
        _states.Add(StateTypes.Jump, new StateJump(StateTypes.Jump, this));
        _states.Add(StateTypes.Fall, new StateFall(StateTypes.Fall, this));
        _states.Add(StateTypes.Dash, new StateDash(StateTypes.Dash, this));
        _states.Add(StateTypes.Slide, new StateSlide(StateTypes.Slide, this));
        _states.Add(StateTypes.Crouch, new StateCrouch(StateTypes.Crouch, this));
        _states.Add(StateTypes.LadderUp, new StateLadderUp(StateTypes.LadderUp, this));
        _states.Add(StateTypes.LadderDown, new StateLadderDown(StateTypes.LadderDown, this));
        _states.Add(StateTypes.Edge, new StateEdge(StateTypes.Edge, this));
        _states.Add(StateTypes.Attack, new StateAttack(StateTypes.Attack, this));
        _states.Add(StateTypes.Hurt, new StateHurt(StateTypes.Hurt, this));
        _states.Add(StateTypes.Die, new StateDie(StateTypes.Die, this));
        _states.Add(StateTypes.Parry, new StateParry(StateTypes.Parry, this));
    }



    //=====================================================================================
    //                             단축키 등록
    //=====================================================================================

    private void RegisterCallbacks()
    {
        _player.OnHpDecrease += () => ChangeState(StateTypes.Hurt);
        _player.OnHpMin += () => ChangeState(StateTypes.Die);
    }

    private void RegisterShortcuts()
    {
        InputHandler.Instance.RegisterKeyPressAction(KeyCode.LeftAlt, () => ChangeState(StateTypes.Jump));
        InputHandler.Instance.RegisterKeyPressAction(KeyCode.LeftShift, () => ChangeState(StateTypes.Dash));
        InputHandler.Instance.RegisterKeyPressAction(KeyCode.X, () => ChangeState(StateTypes.Slide));
        InputHandler.Instance.RegisterKeyPressAction(KeyCode.UpArrow, () =>
        {
            bool success = false;
            success = ChangeState(StateTypes.Edge);
            if (success) return;
            success = ChangeState(StateTypes.LadderUp);
        });
        InputHandler.Instance.RegisterKeyPressAction(KeyCode.DownArrow, () =>
        {
            bool success = false;
            success = ChangeState(StateTypes.LadderDown);
            if (success) return;
            success = ChangeState(StateTypes.Crouch);
        });
        InputHandler.Instance.RegisterKeyPressAction(KeyCode.A, () => ChangeState(StateTypes.Attack));
        InputHandler.Instance.RegisterKeyPressAction(KeyCode.Q, () => ChangeState(StateTypes.Parry));
    }
}
