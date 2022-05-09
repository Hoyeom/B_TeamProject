using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Project.Scripts.Player;

public class BonePrefab2 : MonoBehaviour
{
    public float attackRadius;
    public LayerMask targetLayer;
    public LayerMask wall;
    public float damage;
    public int speed;

    private float randomAngle;
    private float keepBone;

    private void OnEnable()
    {
        randomAngle = UnityEngine.Random.Range(0.5f, 1.5f);
        keepBone = 0;
    }
    private void FixedUpdate()
    {
        Attack();
        Wall();

        transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
        transform.Rotate(0, 0, randomAngle);
        keepBone++;
        if(keepBone>100)
        {
            gameObject.SetActive(false);
        }

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
