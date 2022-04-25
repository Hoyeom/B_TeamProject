using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Player;
using UnityEngine;

public class BoneDividePrefab : MonoBehaviour
{
    public float attackRadius;
    public LayerMask targetLayer;
    public LayerMask wall;
    public float damage;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * 2 * Time.fixedDeltaTime);
        Attack();
        Wall();
    }

    public void Attack()
    {
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, attackRadius, targetLayer))
        {
            if (collider.TryGetComponent<IAttackable>(out IAttackable attackable))
            {
                attackable.AttackChangeHealth(damage);
                ObjectPooler.Instance.DestroyGameObject(gameObject);
            }
        }
    }

    public void Wall()
    {
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, attackRadius, wall))
        {
            ObjectPooler.Instance.DestroyGameObject(gameObject);
        }
    }
}
