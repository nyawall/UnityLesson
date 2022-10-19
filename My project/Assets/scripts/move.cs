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

        // �̵����� = �ӵ� * �ð�
        // �̵����� ��ȭ�� = �ӵ� * �ð� ��ȭ�� 
        //transform.position += dir * Time.fixedDeltaTime;
        transform.Translate(dir * MoveSpeed * Time.fixedDeltaTime, Space.World);
        //transform.position += Vector3.forward * v * Time.fixedDeltaTime;
        //Quaternion
        // �����, ������ 4���� ���ҷ� ǥ���ϱ� ���� ü��
        // eular ������ ����ϸ� ������ ������ ����.
        // ������ : �ΰ��� ���� ��ġ�鼭 (����Ʈ) �������� ����ϴ� ���� [��������]
        // ������ ealar ���� ü�躸�� ������
        transform.Rotate(Vector3.up * _r * RotateSpeed * Time.fixedDeltaTime);

    }
}

