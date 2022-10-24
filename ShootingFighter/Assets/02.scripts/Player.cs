using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float _hp;
    public float Hp
    {
        get
        {
            return _hp;

        }
        set
        {
            if (0 > value)
                value = 0;

            _hp = value;
            _hpBar.value = _hp / _hpMax;
            if (_hp <= 0)
            {
                Destroy(gameObject);
            }


        }
    }
    [SerializeField] private float _hpMax;
    [SerializeField] private Slider _hpBar;
    public void Hurt(float damage)
    {
        Hp -= damage;
    }

    private void Awake()
    {
        Hp = _hpMax;
    }
    
}
    

    /*
    private float _hp;
    
    public float Hp;

    [SerializeField] private float _hpMax = 100.0f;
    [SerializeField] private LayerMask _targetLayer;
    public void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & _targetLayer) > 0)
        {
            if (other.gameObject.TryGetComponent<Player>(out Player player))
            {
                player.Hurt(10.0f);
            }
        }
    }
    private void Awake()
    {
        Hp = _hpMax;

    }
    public void Hurt(float damage)
    {
        Hp -= damage;
    }

    */



