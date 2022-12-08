using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parry 시스템을 위한 투사체 센서
/// </summary>
public class ProjectileDetector : MonoBehaviour
{
    public bool IsDetected => ProjectileDetected;
    public Collider2D ProjectileDetected;
    public Vector2 ProjectileDetectedVelocity
    {
        get
        {
            return ProjectileDetected.GetComponent<Rigidbody2D>().velocity;
        }
    }

    [SerializeField] private Vector2 _offset;
    [SerializeField] private Vector2 _size;
    [SerializeField] private LayerMask _targetLayer;
    private Movement _movement;

    private void Awake()
    {
        _movement = GetComponentInParent<Movement>();
    }

    private void FixedUpdate()
    {
        ProjectileDetected = Physics2D.OverlapBox((Vector2)transform.position + new Vector2(_offset.x * _movement.Direction, _offset.y), _size, 0.0f, _targetLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(_offset.x * _movement.Direction, _offset.y, 0.0f), _size);
    }
}
