using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 20.0f;
    [SerializeField] private LayerMask _targetLayer;
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.fixedDeltaTime);
    }
    /// <summary>
    /// 이 monobehavior 를 컴포넌트로 가지는 게임오브젝트가 가지고있는  collider와
    /// 다른 collider 컴포넌트와 Rigidbody 를 가지고 있는 gameobject 가 만났을 때
    /// 그 다른 collider 의 istrigger 옵션이 true 일 때 해당 colldier 를 인자로 넘겨받는 이벤트함수.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //gameobject 의 layer 프로퍼티는 flag 값이 아님에 유의!
        //그래서 구조체 targetlayer 의 value 와 비교하려면
        // flag 값으로 바꿔주기 위해서 bit-shift 연산을 해야함!

        if ((1<<other.gameObject.layer & _targetLayer)> 0)
        {
            // out 키워드
            // 두개 이상의 함수 반환값이 필요할 때 사용하는 키워드
            // out 인자로 넘겨준 변수에 해당함수 반환시 마지막으로 대입되었던 값이 반환됨.
            if(other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Hurt(10.0f);
            }
            //other.gameObject.GetComponent<Enemy>().Hurt(10.0f);
            Destroy(this.gameObject);
        }
        // 간단하게 테스트 해볼 때 문자열로 레이어 검색해서 사용할 수 있음
        //if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        //{ 
        //}
        
        
    }
    /// <summary>
    /// 충돌 대상이 rigidbody 를 가지고 있어야 호출됨.
    /// 그 대상의 collider의 istrigger 옵션이 false 이면 호출됨.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log(collision.gameObject.name);
    }
}
