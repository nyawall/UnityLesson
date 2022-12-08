using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance;

    private Dictionary<KeyCode, Action> _keyDownActions = new Dictionary<KeyCode, Action>();
    private Dictionary<KeyCode, Action> _keyPressActions = new Dictionary<KeyCode, Action>();
    private Dictionary<KeyCode, Action> _keyUpActions = new Dictionary<KeyCode, Action>();

    public void RegisterKeyDownAction(KeyCode key, Action action)
    {
        if (_keyDownActions.ContainsKey(key))
            _keyDownActions[key] = action;
        else
            _keyDownActions.Add(key, action);
    }

    public void RegisterKeyPressAction(KeyCode key, Action action)
    {
        if (_keyPressActions.ContainsKey(key))
            _keyPressActions[key] = action;
        else
            _keyPressActions.Add(key, action);
    }

    public void RegisterKeyUpAction(KeyCode key, Action action)
    {
        if (_keyUpActions.ContainsKey(key))
            _keyUpActions[key] = action;
        else
            _keyUpActions.Add(key, action);
    }


    //StateMachine _machine;

    // ���� ���ÿ�����
    // KeyCode.LeftAlt �� �Է����� ������ �� �ӽ��� ���¸� Jump�� �ٲ�޶�� �׼��� ���ϰ��ִ�. 
    // �׷� ������ KeyCode Ÿ�� �Է��� ������ �� ������ �׼��� �����ϱ� ���ؼ��� ������� �ڵ带 �ۼ��ϸ� ������? 
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.LeftAlt))
        //{
        //    _machine.ChangeState(StateMachine.StateTypes.Jump);
        //}
        //else if (Input.GetKeyDown(KeyCode.I))
        //{
        //    // todo -> Open Inventory
        //}

        foreach (KeyValuePair<KeyCode, Action> pair in _keyDownActions)
        {
            if (Input.GetKeyDown(pair.Key))
                pair.Value.Invoke();
        }

        foreach (KeyValuePair<KeyCode, Action> pair in _keyPressActions)
        {
            if (Input.GetKey(pair.Key))
                pair.Value.Invoke();
        }

        foreach (KeyValuePair<KeyCode, Action> pair in _keyUpActions)
        {
            if (Input.GetKeyUp(pair.Key))
                pair.Value.Invoke();
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
