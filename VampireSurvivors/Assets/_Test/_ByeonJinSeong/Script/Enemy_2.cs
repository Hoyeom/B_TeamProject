using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.Enemy;
using _Project.Scripts.Player;
using UnityEngine;

/* Test 작업 목록
 * 
 * ---- Script ----
 * 1. 스크립트 대공사....
 * 
 */

public class Enemy_2 : MonoBehaviour, IEnemy
{
    public LayerMask targetLayer;
    private Player _player;
    private Rigidbody2D rigid;
    public GameObject expPrefab;
    public AudioClip hitSoundClip;
    private SpriteRenderer _renderer;
    private Animator _animator;

    private readonly int hashHitAnim = Animator.StringToHash("hitTrigger");
    // private readonly int enemyLayer = 6;
    public float maxHealth;
    private float health;
    public float damage;
    public float attackRadius;
    public float speed;
    public float dropExp;
    private float curSpeed;
    private bool isSlow = false;

    // Test 추가 변수
    public Vector2 size;                 // 공격사정거리
    public GameObject EnemyArrowPrefab;  // 생성된 공격 오브젝트
    private GameObject enemyArrow;       // 생성된 공격 오브젝트
    public float collTime;               // 쿨타임
    public float timeset;                // 쿨타임
    private bool firstShoot;             // 초기 쿨타임용

    private void OnEnable()
    {
        curSpeed = speed;
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
                               (Vector2)(playerPos - pos).normalized * curSpeed * Time.deltaTime);
            _renderer.flipX = playerPos.x > pos.x;

            PlayerSearch();

            yield return new WaitForFixedUpdate();
            rigid.velocity = Vector2.zero;
        }
    }

    void PlayerSearch()
    {
        // 플레이어 기준 사정거리 안 적을 저장하는 변수
        Collider2D col = Physics2D.OverlapBox(transform.position, size, 0, targetLayer);

        // 사정거리안 적이 존재할 경우

        if (col != null)
        {
            curSpeed = 0f;
            Arrow();
        }
        else
        {
            curSpeed = speed;
            // Test 고민중
            timeset += Time.deltaTime;
            timeset %= collTime;
        }
    }

    public void Arrow()
    {
        timeset += !firstShoot ? collTime : Time.deltaTime;
        if (timeset >= collTime)
        {
            timeset = 0;
            _animator.SetBool("Attack", true);
            firstShoot = !firstShoot;
        }
    }

    public void ShootArrow()
    {
        enemyArrow = ObjectPooler.Instance.GenerateGameObject(EnemyArrowPrefab);
        enemyArrow.transform.position = transform.position;

        Vector2 pos = _player.transform.position - enemyArrow.transform.position;
        Vector2 pos1 = _player.transform.position - transform.position;
        float rad = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

        enemyArrow.transform.rotation = Quaternion.Euler(0, 0, rad);
    }

    public void TakeDamage(float damage, Vector2 target)
    {
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
        while (timer > 0 || health < 1)
        {
            timer -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        curSpeed = speed;
        isSlow = false;
    }

}