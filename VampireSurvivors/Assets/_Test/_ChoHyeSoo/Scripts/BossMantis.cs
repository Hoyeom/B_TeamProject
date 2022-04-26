using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMantis : MonoBehaviour
{
    private Player _player;
    private Rigidbody2D rigid;


    public float speed;
    private float curSpeed;
    public float maxHealth;
    private float health;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        rigid = GetComponent<Rigidbody2D>();
        curSpeed = speed;
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
            yield return new WaitForFixedUpdate();
            rigid.velocity = Vector2.zero;
        }
    }
}
