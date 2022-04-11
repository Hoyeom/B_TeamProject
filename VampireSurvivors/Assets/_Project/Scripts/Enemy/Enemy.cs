using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Player;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask targetLayer;
    private Player _player;
    private Rigidbody2D rigid;
    public GameObject expPrefab;
    public AudioClip hitSoundClip;
    private SpriteRenderer _renderer;
    private Animator _animator;
    
    private readonly int hashHitAnim = Animator.StringToHash("hitTrigger");
    private readonly int enemyLayer = 6;
    public float maxHealth;
    private float health;
    public float damage;
    public float attackRadius;
    public float speed;
    public float dropExp;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        health = maxHealth;
        StartCoroutine(Move());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Move()
    {
        while (true)
        {
            Vector2 pos = transform.position;
            Vector2 playerPos = _player.transform.position;
            
            rigid.MovePosition(rigid.position +
                               (Vector2) (playerPos - pos).normalized * speed * Time.deltaTime);
            _renderer.flipX = playerPos.x > pos.x;
            Attack();
            yield return new WaitForFixedUpdate();
            rigid.velocity = Vector2.zero;
        }
    }

    public void Attack()
    {
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, attackRadius, targetLayer))
        {
            if (collider.TryGetComponent<IAttackable>(out IAttackable attackable))
            {
                attackable.AttackChangeHealth(damage);
            }
        }
    }

    public void HitEnemy(float amount,Vector2 target)
    {
        health -= amount;
        rigid.MovePosition(rigid.position + ((Vector2) transform.position - target) * 1 * Time.deltaTime);
        AudioManager.Instance.FXEnemyAudioPlay(hitSoundClip);
        if (health < 1)
        {
            GameObject prefab = ObjectPooler.Instance.GenerateGameObject(expPrefab);
            prefab.transform.position = transform.position;
            prefab.GetComponent<Experience>().DropExp(dropExp);
            ObjectPooler.Instance.DestroyGameObject(gameObject);
            return;
        }
        _animator.SetTrigger(hashHitAnim);
    }
    public void SpeedSlow()
    {
        if (health < 1)
        {
            StopCoroutine(EnemySpeedSlow());
        }

        StartCoroutine(EnemySpeedSlow());
    }
    IEnumerator EnemySpeedSlow()
    {
        //Debug.Log("coroutine start" + Time.time);
        speed = 0.2f;
        yield return new WaitForSecondsRealtime(2.0f);
        speed = 1f;
        //Debug.Log("coroutine end" + Time.time);
    }
}
