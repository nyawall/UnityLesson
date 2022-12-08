using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NepenthesController : EnemyController
{
    [SerializeField] private Vector2 _attackBoxCastCenter;
    [SerializeField] private Vector2 _attackBoxCastSize;
    protected override void AttackBehavior()
    {
        base.AttackBehavior();
        RaycastHit2D hit = Physics2D.BoxCast(origin: Rb.position + new Vector2(_attackBoxCastCenter.x * Direction, _attackBoxCastCenter.y),
                                             size: _attackBoxCastSize,
                                             angle: 0.0f,
                                             direction: Vector2.zero,
                                             distance: 0.0f,
                                             layerMask: TargetLayer);

        if (hit.collider != null)
        {
            Player player = hit.collider.GetComponent<Player>();
            if (player.Invincible == false)
            {
                player.Hurt(gameObject, Enemy.ATK, false);
                player.Knockback();
            }
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (Current == States.Attack)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position + new Vector3(_attackBoxCastCenter.x * Direction, _attackBoxCastCenter.y),
                                _attackBoxCastSize);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + new Vector3(_attackBoxCastCenter.x * Direction, _attackBoxCastCenter.y),
                            _attackBoxCastSize);
    }
}
