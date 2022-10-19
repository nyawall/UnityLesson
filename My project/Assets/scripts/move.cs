using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float MoveSpeed = 3.0f;
    public float RotateSpeed = 360.0f;
    float h;
    float v;
    float _r;
    private void Update()
    {
        // Input.GetKey(KeyCode.UpArrow);
        h = Input.GetAxisRaw("Horizontal");
        Debug.Log(h);
        v = Input.GetAxisRaw("Vertical");
        _r = Input.GetAxis("Mouse X");
    }
    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(h, 0.0f, v).normalized;

        // 이동벡터 = 속도 * 시간
        // 이동벡터 변화량 = 속도 * 시간 변화량 
        //transform.position += dir * Time.fixedDeltaTime;
        transform.Translate(dir * MoveSpeed * Time.fixedDeltaTime, Space.World);
        //transform.position += Vector3.forward * v * Time.fixedDeltaTime;
        //Quaternion
        // 사원수, 각도를 4개의 원소로 표현하기 위한 체계
        // eular 각도를 사용하면 짐벌락 문제가 생김.
        // 짐벌락 : 두개의 축이 겹치면서 (조인트) 자유도를 상실하는 현상 [양자형고리]
        // 연산이 ealar 각도 체계보다 가볍다
        transform.Rotate(Vector3.up * _r * RotateSpeed * Time.fixedDeltaTime);

    }
}

