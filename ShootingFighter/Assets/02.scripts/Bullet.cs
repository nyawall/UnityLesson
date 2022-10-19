using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 20.0f;
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.fixedDeltaTime);
    }
}
