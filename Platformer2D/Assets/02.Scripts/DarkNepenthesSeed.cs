using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkNepenthesSeed : Projectile
{
    [SerializeField] private int _damage = 30;
    

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (1 << collision.gameObject.layer == TargetLayer)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.Hurt(Owner, _damage, false);

                if (collision.gameObject.TryGetComponent(out IKnockbackable knockbackable))
                {
                    knockbackable.Knockback();
                }
            }
        }
    }
}
