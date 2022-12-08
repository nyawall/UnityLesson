using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable, IKnockbackable
{
    private int _hp;
    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value <= 0)
            {
                value = 0;
                OnHpMin?.Invoke();
            }
            else if (value < _hp)
            {
                OnHpDecrease?.Invoke();
            }

            _hp = value;
            _hpSlider.value = (float)value / _hpMax;
        }
    }
    [SerializeField] private int _hpMax;
    public event Action OnHpMin;
    public event Action OnHpDecrease;


    [SerializeField] private Slider _hpSlider;

    public int ATK;

    [SerializeField] private LayerMask _targetLayer;
    private Rigidbody2D _rb;
    private EnemyController _controller;
    [SerializeField] private Vector2 _knockBackForce = new Vector2(0.5f, 0.5f);

    public void Hurt(GameObject hitter, int damage, bool isCritical)
    {
        HP -= damage;
        DamagePopUp.Create(1 << hitter.layer, transform.position + Vector3.up * 0.25f, damage);
    }

    public void Knockback()
    {
        if (_controller.MoveEnable == false)
            return;

        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2(-_knockBackForce.x * _controller.Direction, _knockBackForce.y), ForceMode2D.Impulse);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _controller = GetComponent<EnemyController>();
        HP = _hpMax;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == _targetLayer)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.Invincible == false)
            {
                player.Hurt(gameObject, ATK, false);
                player.Knockback();
            }
        }
    }
}
