using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public void Move(Transform target)
    {
        transform.position = target.position;
    }
}
