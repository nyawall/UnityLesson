using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Attribute
    // 컴파일러에게 메타 데이터를 제공해야 할 때 필드/ 프로퍼티 / 클래스 등의 앞에 명시 해 준다

    // serializeField Attribute
    // 유니티 에디터의 인스펙터창에 해당 필드를 노출시키는 attribute
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector3 _boundCenter;
    [SerializeField] private Vector3 _boundSize;

    float _h, _v;
    Vector3 _dir;
    private float _minX, _maxX, _minZ, _maxZ;

    private void Awake()
    {
        _minX = _boundCenter.x - _boundSize.x /2.0f;
        _maxX = _boundCenter.x + _boundSize.x /2.0f;
        _minZ = _boundCenter.z - _boundSize.z /2.0f;
        _maxZ = _boundCenter.z + _boundSize.z /2.0f;
        
    }

    private void FixedUpdate()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        if ((_h < 0 && transform.position.x < _minX) || 
            (_h > 0 && transform.position.x > _maxX)) _h = 0;
        if ((_v < 0 && transform.position.z < _minZ) || 
            (_v > 0 && transform.position.z > _maxZ)) _v = 0;
       

        _dir = new Vector3(_h, 0.0f, _v).normalized;
        transform.Translate(_dir * _moveSpeed * Time.fixedDeltaTime);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boundCenter, _boundSize);
    }
}
