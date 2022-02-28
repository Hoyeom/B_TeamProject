using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _player;
    private Rigidbody2D rigid;
    public GameObject expPrefab;
    private GameObject tempPrefab;
    public AudioClip hitSoundClip;
    private SpriteRenderer _renderer;
    
    public float health;
    public float damage;
    public float speed;
    public float dropExp;

    private void Awake()
    {
        tempPrefab = Instantiate(expPrefab, transform);
        tempPrefab.SetActive(false);
        _player = FindObjectOfType<Player>();
        rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            Vector2 pos = transform.position;
            Vector2 playerPos = _player.transform.position;
            
            rigid.AddForce(/*rigid.position +*/
                               (Vector2) (playerPos - pos).normalized * speed * Time.fixedDeltaTime,ForceMode2D.Impulse);
            _renderer.flipX = playerPos.x > pos.x;
            yield return new WaitForSeconds(5);
            rigid.velocity = Vector2.zero;
        }
    }

    public float Attack() => damage;

    public void HitEnemy(float amount)
    {
        health -= amount;
        AudioManager.instance.AudioPlay(hitSoundClip);
        if (health < 1)
        {
            tempPrefab.transform.position = transform.position;
            tempPrefab.transform.parent = null;
            tempPrefab.SetActive(true);
            Destroy(gameObject);
        }

        StartCoroutine(HitAnimation());
    }

    IEnumerator HitAnimation()
    {
        _renderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        while (_renderer.color != Color.white)
        {
            _renderer.color = Color.Lerp(_renderer.color, Color.white, 0.02f);
            yield return null;
        }

        yield return null;
    }
}
