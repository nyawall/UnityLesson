using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetector : MonoBehaviour
{
    private bool _isDetected;
    public bool IsDetected
    {
        get
        {
            return _isDetected;
        }
        set
        {
            if (value &&
                _isDetected != value)
            {
                DetectedPosition = (Vector2)transform.position + new Vector2(_bottomOffset.x * _movement.Direction, _bottomOffset.y);
            }
            _isDetected = value;
        }
    }
    public Vector2 DetectedPosition; 
    public bool TopOn;
    public bool BottomOn;

    [SerializeField] private Vector2 _topOffset;
    [SerializeField] private Vector2 _bottomOffset;
    [SerializeField] private LayerMask _groundLayer;
    private Movement _movement;

    private void Awake()
    {        
        _movement = GetComponentInParent<Movement>();
    }

    private void FixedUpdate()
    {
        TopOn = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(_topOffset.x * _movement.Direction, _topOffset.y),
                                        0.01f,
                                        _groundLayer);

        BottomOn = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(_bottomOffset.x * _movement.Direction, _bottomOffset.y),
                                           0.01f,
                                           _groundLayer);

        IsDetected = TopOn == false && BottomOn;
    }


    private void OnDrawGizmos()
    {
        if (_movement == null)
            return;
        
        Gizmos.color = Color.blue;
        if (TopOn)
            Gizmos.DrawSphere(transform.position + new Vector3(_topOffset.x * _movement.Direction, _topOffset.y, 0.0f), 0.01f);
        else
            Gizmos.DrawWireSphere(transform.position + new Vector3(_topOffset.x * _movement.Direction, _topOffset.y, 0.0f), 0.01f);

        Gizmos.color = Color.red;
        if (BottomOn)
            Gizmos.DrawSphere(transform.position + new Vector3(_bottomOffset.x * _movement.Direction, _bottomOffset.y, 0.0f), 0.01f);
        else
            Gizmos.DrawWireSphere(transform.position + new Vector3(_bottomOffset.x * _movement.Direction, _bottomOffset.y, 0.0f), 0.01f);
    }
}
