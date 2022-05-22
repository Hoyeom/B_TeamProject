using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Enemy;
using _Project.Scripts.Player;
using UnityEngine;

public class PlantWall : MonoBehaviour
{
    public LayerMask targetLayer;
    public LayerMask wall;
    private Player _player;
    private Rigidbody2D rigid;
    public AudioClip hitSoundClip;
    private SpriteRenderer _renderer;
    private Animator _animator;

    private float curSpeed;
    public float maxHealth;
    private float health;
    public float damage;
    public float attackRadius;
    public float speed;

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

    IEnumerator Move()
    {
        while (true)
        {
            Wall();
            Attack();

            Vector2 pos = transform.position;
            Vector2 playerPos = _player.transform.position;

            rigid.MovePosition(rigid.position +
                                   (Vector2)(playerPos - pos).normalized * curSpeed * Time.deltaTime);
            _renderer.flipX = playerPos.x > pos.x;

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
            ObjectPooler.Instance.DestroyGameObject(gameObject);
            return;
        }
        //_animator.SetTrigger(hashHitAnim);
    }

    public void Wall()
    {
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, attackRadius, wall))
        {
            ObjectPooler.Instance.DestroyGameObject(gameObject);
        }
    }
}
