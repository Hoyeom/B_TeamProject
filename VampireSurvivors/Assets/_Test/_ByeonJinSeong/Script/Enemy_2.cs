using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.Enemy;
using _Project.Scripts.Player;
using UnityEngine;

/* Test 작업 목록
 * ---- Anim ----
 * 1. sprite 찾기
 * 2. 애니메이션만들기(Move, hit)
 * 
 * ---- Script ----
 * 3. Player 공격 사거리 기능 (노가다?... 이벤트? >> 이벤트로 할거면 비둘기도 바꿔야하나?)
 * 4. L62 공격 방식 수정
 * 5. 원거리 공격 오브젝트 (Pool로 생성)
 * 
 * ---- 참고 ----
 * 6. gamemanager에서 생성 중
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
        //_animator = GetComponent<Animator>();
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
            Attack();
            yield return new WaitForFixedUpdate();
            rigid.velocity = Vector2.zero;
        }
    }

    public void Attack()
    {
        //foreach (var collider in Physics2D.OverlapCircleAll(transform.position, attackRadius, targetLayer))
        //{
        //    if (collider.TryGetComponent<IAttackable>(out IAttackable attackable))
        //    {
        //        attackable.AttackChangeHealth(damage);
        //    }
        //}
    }

    public void HitEnemy(float damage, Vector2 target)
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
        //임시 삭제 _animator.SetTrigger(hashHitAnim);
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