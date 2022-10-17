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
    private void Start()
    {
        Debug.Log("[test] : Start");
    }

    private void Update()
    {
        Debug.Log("[test] : Update");
    }

    private void LateUpdate()
    {
        Debug.Log("[Test] : LateUpdate");
    }
    private void FixedUpdate()
    {
        Debug.Log("[Test] : FixedUpdate");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
    private void OnGUI()
    {
        Event e = Event.current;
        Debug.Log(e.mousePosition);
    }
    private void OnApplicationPause(bool pause)
    {
        Debug.Log($"[Test] : paused = {pause}");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("[Test] : applicationQuit");
    }

    private void OnDisable()
    {
        Debug.Log("[test] : disable");
    }
    private void OnDestroy()
    {
        Debug.Log("[test] : Destroyed");
    }
}
