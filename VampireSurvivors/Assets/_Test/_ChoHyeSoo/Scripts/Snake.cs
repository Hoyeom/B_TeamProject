using _Project.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float speed = 3f;
    public float attackRadius = 0.2f;
    [SerializeField] private FMSpecSO monsterSpec;

    private void FixedUpdate()
    {
        Attack();
        transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
    }

    public void Attack()
    {
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, attackRadius, monsterSpec.TarGetLayer))
        {
            if (collider.TryGetComponent<IAttackable>(out IAttackable attackable))
            {
                attackable.AttackChangeHealth(monsterSpec.Damage);
                ObjectPooler.Instance.DestroyGameObject(gameObject);
            }
        }
    }
}
