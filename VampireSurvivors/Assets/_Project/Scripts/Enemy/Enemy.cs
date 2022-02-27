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
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position +
                           (Vector2) (_player.transform.position - transform.position).normalized * speed * Time.fixedDeltaTime);
    }

    public float Attack() => damage;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        
        tempPrefab.transform.position = transform.position;
        tempPrefab.transform.parent = null;
        tempPrefab.SetActive(true);
    }
}
