using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.Enemy;
using _Project.Scripts.Player;
using UnityEngine;

public class SkullBoss2 : MonoBehaviour
{
    public LayerMask targetLayer;
    private Player _player;
    private Rigidbody2D rigid;
    public GameObject expPrefab;
    public AudioClip hitSoundClip;
    private SpriteRenderer _renderer;
    private Animator _animator;

    public int fire_rate;  //프레임기준 공격딜레이
    private int shoot_time;
    public GameObject attackPrefab;
    public GameObject enemyObject;

    private readonly int hashHitAnim = Animator.StringToHash("hitTrigger");
    private readonly int enemyLayer = 6;
    public float maxHealth;
    private float health;
    public float damage;
    public float attackRadius;
    public float speed;
    public float dropExp;
    private float curSpeed;
    private bool isSlow = false;

    private void OnEnable()
    {
        curSpeed = speed;
        _player = FindObjectOfType<Player>();
        rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        health = maxHealth;
        StartCoroutine(Move());

        shoot_time = fire_rate;
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

            shoot_time++;
            if (shoot_time % fire_rate == 0)
            {
                Fire();
                yield return new WaitForSeconds(3f);
                _renderer.flipX = playerPos.x > pos.x;
                Spawn();
            }
            else
            {
                rigid.MovePosition(rigid.position +
                                   (Vector2)(playerPos - pos).normalized * curSpeed * Time.deltaTime);
                _renderer.flipX = playerPos.x > pos.x;
            }
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

    public void Spawn()
    {
        GameObject obj = ObjectPooler.Instance.GenerateGameObject(enemyObject);
        obj.transform.position = transform.position;
        obj.transform.Translate(Vector2.one * UnityEngine.Random.Range(-.3f, .3f));
        teleport();
    }

    public void teleport()
    {
        this.transform.position = new Vector2(0, 0);
    }

    private void Fire()
    {
        GameObject skull_1 = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        skull_1.transform.position = transform.position;
        skull_1.transform.LookAt(_player.transform);
        skull_1.transform.Rotate(0, 90, 0);

        GameObject skull_2 = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        skull_2.transform.position = transform.position;
        skull_2.transform.LookAt(_player.transform);
        skull_2.transform.Rotate(0, 90, 72);

        GameObject skull_3 = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        skull_3.transform.position = transform.position;
        skull_3.transform.LookAt(_player.transform);
        skull_3.transform.Rotate(0, 90, 144);

        GameObject skull_4 = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        skull_4.transform.position = transform.position;
        skull_4.transform.LookAt(_player.transform);
        skull_4.transform.Rotate(0, 90, -144);

        GameObject skull_5 = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        skull_5.transform.position = transform.position;
        skull_5.transform.LookAt(_player.transform);
        skull_5.transform.Rotate(0, 90, -72);
    }

    public void TakeDamage(float damage, Vector2 target)
    {
        if (health < 1)
        { return; }

        UIManager.Instance.SpawnDamageText((int)damage, transform.position);
        health -= damage;

        rigid.MovePosition(rigid.position + ((Vector2)transform.position - target) * 1 * Time.deltaTime);
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

    public void SpeedSlow(float slow, float time)
    {
        if (isSlow || health < 1) { return; }

        isSlow = true;

        curSpeed = speed;

        StartCoroutine(EnemySpeedSlow(slow, time));
    }

    IEnumerator EnemySpeedSlow(float slow, float time)
    {
        float timer = time;
        curSpeed *= slow;
        Debug.Log($"적 현재 속도 {curSpeed}");
        while (timer > 0 || health < 1)
        {
            timer -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        curSpeed = speed;
        isSlow = false;
    }
}
