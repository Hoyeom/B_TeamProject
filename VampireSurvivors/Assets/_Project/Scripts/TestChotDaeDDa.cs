using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChotDaeDDa : MonoBehaviour
{
    public float  dropExp;
    public GameObject expPrefab;
    private Rigidbody2D rigid;
    private Transform player;
    public float speed;
    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position +
                           (Vector2) (player.position - transform.position) * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameObject ASsii = Instantiate(expPrefab);
            ASsii.GetComponent<Experience>().DropExp(dropExp);
            ASsii.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
