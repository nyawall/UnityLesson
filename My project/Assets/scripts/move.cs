using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    float h;
    float v;
    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        Debug.Log(h);
        v = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(h, 0.0f, v).normalized;
        transform.position += dir * Time.fixedDeltaTime;
        //transform.position += Vector3.forward * v * Time.fixedDeltaTime;
    }
}

