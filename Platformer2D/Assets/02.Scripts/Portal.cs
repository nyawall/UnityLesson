using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public static bool IsBusy;
    [SerializeField] private Portal _destination;
    [SerializeField] private GameObject _map;
    [SerializeField] private BoxCollider2D _mapBoundShape;
    private LayerMask _targetLayer;

    private void Awake()
    {
        _targetLayer = 1 << LayerMask.NameToLayer("Player");
    }

    private void MoveToDestination(GameObject passenger)
    {
        passenger.SetActive(false);
        _map.SetActive(false);
        _destination._map.SetActive(true);
        passenger.transform.position = _destination.transform.position;
        CameraHandler.Instance.BoundShape = _destination._mapBoundShape;
        passenger.SetActive(true);
        Invoke("Ready", 1.0f);
    }

    private void Ready() => IsBusy = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsBusy == false &&
            Input.GetKey(KeyCode.UpArrow) && 
            1<<collision.gameObject.layer == _targetLayer)
        {
            IsBusy = true;
            MoveToDestination(collision.gameObject);
        }
    }
}
