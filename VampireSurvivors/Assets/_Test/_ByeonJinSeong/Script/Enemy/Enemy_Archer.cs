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
 * 1. 스크립트 구조 변경 .... (50%)
 * 1-1. 공통 함수 고민
 * 1-2. 미라 몬스터 변경?.
 * 
 * 2. 1. size형식 전부 묶기?...
 */

public class Enemy_Archer : Enemy, IEnemy
{
    // Test 한번만 호출?
    private Player _player;
    private Rigidbody2D rigid;
    private SpriteRenderer _renderer;
    private Animator _animator;

    // Test 추가 변수
    public Vector2 size;
    private bool firstShoot;             // 초기 쿨타임용
    private float timeset;                // 쿨타임

    public EnemySO enemySo;               // Test 접근지정자 변경 고민
    public EnemyPrefabSO enemyPrefabSo;

    private float curSpeed;               // 수정 여부 고민
    private float health;    
    private bool isSlow = false;

    private readonly int hashHitAnim = Animator.StringToHash("hitTrigger");
    private readonly int hashAttackAnim = Animator.StringToHash("Attack");

    private void OnEnable()
    {
        curSpeed = enemySo.MosterSpeed;
        health = enemySo.MaxHealth;

        _player = FindObjectOfType<Player>();
        rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

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
        Collider2D col = Physics2D.OverlapBox(transform.position, size, 0, enemyPrefabSo.TarGetLayer);

        if (col != null)
        {
            curSpeed = 0f;
            Arrow();
        }
        else
        {
            curSpeed = enemySo.MosterSpeed;
            _animator.SetBool(hashAttackAnim, false);

            // Test 고민중
            timeset += Time.deltaTime;
            timeset %= enemySo.CollTime;
        }
    }

    public void Arrow()
    {
        timeset += !firstShoot ? enemySo.CollTime : Time.deltaTime;   // Test 낭비같음..
        if (timeset >= enemySo.CollTime)
        {
            timeset = 0;
            _animator.SetBool(hashAttackAnim, true);
            firstShoot = !firstShoot;
        }
    }

    public void ShootArrow()
    {
        GameObject enemyArrow = ObjectPooler.Instance.GenerateGameObject(enemyPrefabSo.ArcherArrow);
        enemyArrow.transform.position = transform.position;

        Vector2 pos = _player.transform.position - enemyArrow.transform.position;
        float rad = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

        enemyArrow.transform.rotation = Quaternion.Euler(0, 0, rad);
    }

    public override void  TakeDamage(float damage, Vector2 target)
    {
        health -= damage;
        rigid.MovePosition(rigid.position + ((Vector2)transform.position - target) * 1 * Time.deltaTime);
        Managers.Audio.FXEnemyAudioPlay(enemyPrefabSo.HitSoundClip);
        if (health < 1)
        {
            GameObject prefab = ObjectPooler.Instance.GenerateGameObject(enemyPrefabSo.ExpPrefab);
            prefab.transform.position = transform.position;
            prefab.GetComponent<Experience>().DropExp(enemySo.DropExp);
            Managers.Game.Room.killMonsterCount++;
            ObjectPooler.Instance.DestroyGameObject(gameObject);
            return;
        }
        _animator.SetTrigger(hashHitAnim);
    }

    public override void SpeedSlow(float slow, float time)
    {
        if (isSlow || health < 1) { return; }

        isSlow = true;

        curSpeed = enemySo.MosterSpeed;

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
        curSpeed = enemySo.MosterSpeed;
        isSlow = false;
    }
}