using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Project.Scripts.Player;

public class BonePrefab : MonoBehaviour
{
    public GameObject attackPrefab;

    public float attackRadius;
    public LayerMask targetLayer;
    public LayerMask wall;
    public float damage;

    private int divide; //분할 횟수
    private int divide_angle;
    private float delay;    //분할 주기
    private float delayRandom;

    private void OnEnable()
    {
        delayRandom = UnityEngine.Random.Range(50, 100);
        delay = 0;
        divide = 1;
    }
    private void FixedUpdate()
    {
        Attack();
        Wall();

        transform.Translate(Vector2.left * 4 * Time.fixedDeltaTime);
        delay++;
        if (divide > 0 && delay>delayRandom)
        {
            
            divide_angle = UnityEngine.Random.Range(0, 45); //0~45 랜덤
            divide--;
    
            GameObject skull_1 = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
            skull_1.transform.position = transform.position;
            skull_1.transform.Rotate(0, 0, divide_angle);

            GameObject skull_2 = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
            skull_2.transform.position = transform.position;
            skull_2.transform.Rotate(0, 0, divide_angle+90);

            GameObject skull_3 = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
            skull_3.transform.position = transform.position;
            skull_3.transform.Rotate(0, 0, divide_angle+180);

            GameObject skull_4 = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
            skull_4.transform.position = transform.position;
            skull_4.transform.Rotate(0, 0, divide_angle+270);

            ObjectPooler.Instance.DestroyGameObject(gameObject);
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
