using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
    public float speed;
    public float dropExp;

    private void Awake()
    {
        // tempPrefab = Instantiate(expPrefab, transform);
        _player = FindObjectOfType<Player>();
        rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        health = maxHealth;
        StartCoroutine(Move());
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
            yield return new WaitForSeconds(0.05f);
            rigid.velocity = Vector2.zero;
        }
    }

    public float Attack() => damage;

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
}
