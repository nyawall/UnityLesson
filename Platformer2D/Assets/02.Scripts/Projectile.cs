using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public GameObject Owner;
    public Vector3 Dir;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _mapBoundLayer;
    [SerializeField] protected LayerMask TargetLayer;
    private Rigidbody2D _rb;
    public void SetUp(GameObject owner, Vector3 dir, float speed, LayerMask selfLayer, LayerMask targetLayer)
    {
        Owner = owner;
        Dir = dir;
        _speed = speed;
        gameObject.layer = selfLayer;
        TargetLayer = targetLayer;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = Dir * _speed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == _mapBoundLayer)
            Destroy(gameObject);
    }
}
