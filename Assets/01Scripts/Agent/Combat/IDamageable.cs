using UnityEngine;

public interface IDamageable
{
    void ApplyDamage(float damage, Vector2 direction, float knockBackForce, Agent dealer);
}
