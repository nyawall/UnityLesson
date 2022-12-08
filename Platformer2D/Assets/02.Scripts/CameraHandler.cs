using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler Instance;

    [SerializeField] private Vector2 _offset;
    [Range(1.0f, 10.0f)]
    [SerializeField] private float _smoothness;
    private Camera _camera;

    [SerializeField] private BoxCollider2D _boundShape;
    public BoxCollider2D BoundShape
    {
        get
        {
            return _boundShape;
        }
        set
        {
            _boundShape = value;
            _boundShapeXMin = value.transform.position.x + value.offset.x - value.size.x / 2.0f;
            _boundShapeXMax = value.transform.position.x + value.offset.x + value.size.x / 2.0f;
            _boundShapeYMin = value.transform.position.y + value.offset.y - value.size.y / 2.0f;
            _boundShapeYMax = value.transform.position.y + value.offset.y + value.size.y / 2.0f;            
        }
    }
    private float _boundShapeXMin;
    private float _boundShapeXMax;
    private float _boundShapeYMin;
    private float _boundShapeYMax;

    [SerializeField] private Transform _target;


    private void Awake()
    {
        Instance = this;
        _camera = Camera.main;
        BoundShape = _boundShape;
    }

    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPos = new Vector3(_target.position.x, _target.position.y, _camera.transform.position.z)
                            + (Vector3)_offset;
        Vector3 smoothPos = Vector3.Lerp(_camera.transform.position, targetPos, _smoothness * Time.deltaTime);

        Vector3 leftBottom = _camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, _camera.nearClipPlane));
        Vector3 rightTop = _camera.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, _camera.nearClipPlane));
        Vector3 size = new Vector3(rightTop.x - leftBottom.x, rightTop.y - leftBottom.y, 0.0f);

        // X 최소 경계 
        if (smoothPos.x < _boundShapeXMin + size.x / 2.0f)
            smoothPos.x = _boundShapeXMin + size.x / 2.0f;
        // X 최대 경계
        else if (smoothPos.x > _boundShapeXMax - size.x / 2.0f)
            smoothPos.x = _boundShapeXMax - size.x / 2.0f;

        // Y 최소 경계 
        if (smoothPos.y < _boundShapeYMin + size.y / 2.0f)
            smoothPos.y = _boundShapeYMin + size.y / 2.0f;
        // Y 최대 경계
        else if (smoothPos.y > _boundShapeYMax - size.y / 2.0f)
            smoothPos.y = _boundShapeYMax - size.y / 2.0f;

        _camera.transform.position = smoothPos;
    }

    private void OnDrawGizmosSelected()
    {
        Camera cam = Camera.main;
        Vector3 leftBottom = cam.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, cam.nearClipPlane));
        Vector3 rightTop = cam.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, cam.nearClipPlane));
        Vector3 center = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cam.nearClipPlane));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, rightTop - leftBottom);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(BoundShape.transform.position + (Vector3)BoundShape.offset, BoundShape.size);
    }
}
