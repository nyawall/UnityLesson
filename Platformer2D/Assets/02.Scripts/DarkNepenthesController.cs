using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkNepenthesController : EnemyController
{
    [SerializeField] private GameObject _seedPrefab;
    [SerializeField] private Vector2 _seedSpawnOffset;
    protected override void AttackBehavior()
    {
        base.AttackBehavior();
        Projectile projectile = Instantiate(_seedPrefab,
                                            Rb.position + new Vector2(_seedSpawnOffset.x * Direction, _seedSpawnOffset.y),
                                            Quaternion.identity).GetComponent<Projectile>();

        projectile.Owner = gameObject;
        projectile.Dir = Vector3.right * Direction;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position + (Vector3)_seedSpawnOffset, 0.01f);
    }
}
