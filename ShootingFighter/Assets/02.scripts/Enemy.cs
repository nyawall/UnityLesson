using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _hp;

    // ������Ƽ (Property)
    // C# ���� �ʵ��� ĸ��ȭ�� ���� ����
    // Geeter, Seeter �����ڸ� �����ؼ� ���� �аų� �� �� �ѹ� ������ �� �ִ�.
    // ĸ��ȭ : ĸ���˾�ó�� �߸��� �� ������ ���ų� ���ٽ� Ư���� ������ �����ϵ��� ����� �۾�
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
            if (_hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    [SerializeField] private float _hpMax = 100.0f;
    private void Awake()
    {
        Hp = _hpMax;
        
    }
    
    public void Hurt(float damage)
    {
        Hp -= damage;
    }
    public void SetHp(float value)
    {
       
    }
    
}
