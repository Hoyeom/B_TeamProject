using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Project.Scripts.Player;

public class Seed : MonoBehaviour
{
    public float attackRadius;
    public LayerMask targetLayer;
    public LayerMask wall;
    public float damage;

    public int speed;
    private void FixedUpdate()
    {
        Attack();
        Wall();

        transform.Translate(Vector2.right * Time.fixedDeltaTime * speed * Random.Range(1f,2f));
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
