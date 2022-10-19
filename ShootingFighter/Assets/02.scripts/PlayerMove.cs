using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Attribute
    // �����Ϸ����� ��Ÿ �����͸� �����ؾ� �� �� �ʵ�/ ������Ƽ / Ŭ���� ���� �տ� ��� �� �ش�

    // serializeField Attribute
    // ����Ƽ �������� �ν�����â�� �ش� �ʵ带 �����Ű�� attribute
    [SerializeField] private float _moveSpeed;

    float _h, _v;
    Vector3 _dir;

    private void FixedUpdate()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        _dir = new Vector3(_h, 0.0f, _v).normalized;
        transform.Translate(_dir * _moveSpeed * Time.fixedDeltaTime);

    }
}
