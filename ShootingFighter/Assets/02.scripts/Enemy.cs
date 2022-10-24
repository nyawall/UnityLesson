using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float _hp;

    // 프로퍼티 (Property)
    // C# 에서 필드의 캡슐화를 위한 문법
    // Geeter, Seeter 접근자를 제공해서 값을 읽거나 쓸 때 한번 래핑할 수 있다.
    // 캡슐화 : 캡슐알약처럼 잘못된 값 접근을 막거나 접근시 특별한 연산을 수행하도록 만드는 작업
    public float Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value < 0)
                value = 0;

            _hp = value;
            _hpBar.value = _hp / _hpMax;
            if (_hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    [SerializeField] private float _hpMax = 100.0f;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _damage = 20.0f;




    //=============================================================================================================
    //*****************************************private Methods*****************************************************
    //=============================================================================================================
    private void Awake()
    {
        Hp = _hpMax;
        
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1<<other.gameObject.layer& _targetLayer)>0)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.Hurt(_damage);
                Destroy(gameObject);
            }
        }
    }
    //=============================================================================================================
    //*********************************************get Methods*****************************************************
    //=============================================================================================================
    public void Hurt(float damage)
    {
        Hp -= damage;
    } 
    public void SetHp(float value)
    {
       
    }
    
}
