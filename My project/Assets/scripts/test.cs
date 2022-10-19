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
    /// update 함수가 호출되기 전에 한번 실행되는 함수
    /// </summary>
    private void Start()
    {
        Debug.Log("[test] : Start");
    }
    /// <summary>
    /// 게임 로직 매 프레임 처음 마다 호출되는 함수
    /// </summary>
    private void Update()
    {
        Debug.Log("[test] : Update");
    }
    /// <summary>
    /// 게임 로직 매 프레임 마지막 마다 호출되는 함수
    /// </summary>
    private void LateUpdate()
    {
        Debug.Log("[Test] : LateUpdate");
    }
    /// <summary>
    /// 물리 연산을 위한 프레임 마다 호출되는 함수
    /// </summary>
    private void FixedUpdate()
    {
        Debug.Log("[Test] : FixedUpdate");
    }

    /// <summary>
    ///  editor 에서  gizmo(에디터상의 그래픽적인 부가 요소들)들을 그릴 때 호출되는 함수
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }

    /// <summary>
    /// UI이벤트를 처리하기 위한 함수
    /// </summary>
    private void OnGUI()
    {
        Event e = Event.current;
        Debug.Log(e.mousePosition);
    }
    /// <summary>
    /// 어플리케이션이 일시정지 될때 호출
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        Debug.Log($"[Test] : paused = {pause}");
    }


    /// <summary>
    ///  어플리케이션이 종료될때 호출
    /// </summary>
    private void OnApplicationQuit()
    {
        Debug.Log("[Test] : applicationQuit");
    }

    /// <summary>
    /// Script Instance 가 비활성화될 때 마다 호출
    /// </summary>
    private void OnDisable()
    {
        Debug.Log("[test] : disable");
    }

    /// <summary>
    /// 파괴될 때 호출
    /// </summary>
    private void OnDestroy()
    {
        Debug.Log("[test] : Destroyed");
    }
}
