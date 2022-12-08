using UnityEngine;

public interface IDamageable
{
    public void Hurt(GameObject owner, int damage, bool isCritical);
}