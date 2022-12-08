using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable, IKnockbackable
{
    public bool Invincible;
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
    private Rigidbody2D _rb;
    private Movement _movement;
    [SerializeField] private Vector2 _knockBackForce = new Vector2(0.5f, 0.5f);

    public void Hurt(GameObject hitter, int damage, bool isCritical)
    {
        if (Invincible)
            return;

        HP -= damage;
        DamagePopUp.Create(1 << hitter.layer, transform.position + Vector3.up * 0.25f, damage);
        Invincible = true;
        StartCoroutine(E_SetInvincibleAfterSeconds(false, 1.0f));   
    }

    public void Knockback()
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2(-_knockBackForce.x * _movement.Direction, _knockBackForce.y), ForceMode2D.Impulse);
    }

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _rb = GetComponent<Rigidbody2D>();
        HP = _hpMax;
    }

    private void OnDisable()
    {
        Invincible = false;
    }

    IEnumerator E_SetInvincibleAfterSeconds(bool invincible, float sec)
    {
        yield return new WaitForSeconds(sec);
        Invincible = invincible;
    }
}
