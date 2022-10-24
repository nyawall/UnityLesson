using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBound : MonoBehaviour
{
    

    [SerializeField] private LayerMask _targetLayer;
    private void OnTriggerEnter(Collider other)
    {

        if ((1<<other.gameObject.layer & _targetLayer) >0)
        {
            Destroy(other.gameObject);
        }
    }
}
