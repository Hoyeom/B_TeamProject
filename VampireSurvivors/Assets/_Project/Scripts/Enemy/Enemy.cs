using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.Enemy;
using _Project.Scripts.Player;
using UnityEngine;

public abstract class Enemy : MonoBehaviour,IEnemy
{
    //public LayerMask targetLayer;
    //private Player _player;
    //private Rigidbody2D rigid;
    //public GameObject expPrefab;
    //public AudioClip hitSoundClip;
    //private SpriteRenderer _renderer;
    //private Animator _animator;
    
    //private readonly int hashHitAnim = Animator.StringToHash("hitTrigger");
    //public float maxHealth;
    //private float health;
    //public float damage;
    //public float attackRadius;
    //public float speed;
    //public float dropExp;
    //private float curSpeed;
    //private bool isSlow = false;
    
    //private void OnEnable()
    //{
    //    curSpeed = speed;
    //    _player = FindObjectOfType<Player>();
    //    rigid = GetComponent<Rigidbody2D>();
    //    _renderer = GetComponent<SpriteRenderer>();
    //    _animator = GetComponent<Animator>();
    //    health = maxHealth;
    //    StartCoroutine(Move());
    //}

    //private void OnDisable()
    //{
    //    StopAllCoroutines();
    //}

    //IEnumerator Move()
    //{
    //    while (true)
    //    {
    //        Vector2 pos = transform.position;
    //        Vector2 playerPos = _player.transform.position;
            
    //        rigid.MovePosition(rigid.position +
    //                           (Vector2) (playerPos - pos).normalized * curSpeed * Time.deltaTime);
    //        _renderer.flipX = playerPos.x > pos.x;
    //        Attack();
    //        yield return new WaitForFixedUpdate();
    //        rigid.velocity = Vector2.zero;
    //    }
    //}

    public virtual void Attack() { }

    public virtual void TakeDamage(float damage, Vector2 target) 
    {
    }
    
    public virtual void SpeedSlow(float slow, float time) { }
}