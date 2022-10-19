using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public int Num;

    private void Awake()
    {
        Debug.Log("[Test] : Awake");
    }
    private void OnEnable()
    {
        Debug.Log("[test] : OnEnable");
    }

    
    private void Reset()
    {
        Debug.Log("[TEST] : Reset");
    }
    /// <summary>
    /// update �Լ��� ȣ��Ǳ� ���� �ѹ� ����Ǵ� �Լ�
    /// </summary>
    private void Start()
    {
        Debug.Log("[test] : Start");
    }
    /// <summary>
    /// ���� ���� �� ������ ó�� ���� ȣ��Ǵ� �Լ�
    /// </summary>
    private void Update()
    {
        Debug.Log("[test] : Update");
    }
    /// <summary>
    /// ���� ���� �� ������ ������ ���� ȣ��Ǵ� �Լ�
    /// </summary>
    private void LateUpdate()
    {
        Debug.Log("[Test] : LateUpdate");
    }
    /// <summary>
    /// ���� ������ ���� ������ ���� ȣ��Ǵ� �Լ�
    /// </summary>
    private void FixedUpdate()
    {
        Debug.Log("[Test] : FixedUpdate");
    }

    /// <summary>
    ///  editor ����  gizmo(�����ͻ��� �׷������� �ΰ� ��ҵ�)���� �׸� �� ȣ��Ǵ� �Լ�
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }

    /// <summary>
    /// UI�̺�Ʈ�� ó���ϱ� ���� �Լ�
    /// </summary>
    private void OnGUI()
    {
        Event e = Event.current;
        Debug.Log(e.mousePosition);
    }
    /// <summary>
    /// ���ø����̼��� �Ͻ����� �ɶ� ȣ��
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        Debug.Log($"[Test] : paused = {pause}");
    }


    /// <summary>
    ///  ���ø����̼��� ����ɶ� ȣ��
    /// </summary>
    private void OnApplicationQuit()
    {
        Debug.Log("[Test] : applicationQuit");
    }

    /// <summary>
    /// Script Instance �� ��Ȱ��ȭ�� �� ���� ȣ��
    /// </summary>
    private void OnDisable()
    {
        Debug.Log("[test] : disable");
    }

    /// <summary>
    /// �ı��� �� ȣ��
    /// </summary>
    private void OnDestroy()
    {
        Debug.Log("[test] : Destroyed");
    }
}
