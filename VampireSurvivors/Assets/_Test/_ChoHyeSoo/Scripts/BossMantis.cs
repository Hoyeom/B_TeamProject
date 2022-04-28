using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMantis : MonoBehaviour
{
    private Player _player;
    private Rigidbody2D rigid;
    public GameObject enemyObject;
    private SpriteRenderer _renderer;

    //private Animator anim = null;


    public float speed;
    public float maxHealth;
    public int fire_rate;  //프레임기준 공격딜레이

    private int shoot_time;
    private float curSpeed;
    private float health;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();

        //anim = GetComponent<Animator>();

        curSpeed = speed;
        health = maxHealth;
        shoot_time = fire_rate;

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
            
            shoot_time++;
            if (shoot_time % fire_rate == 0)
            {

                rigid.MovePosition(rigid.position +
                                   (Vector2)(playerPos - pos).normalized * curSpeed*200 * Time.deltaTime);

                yield return new WaitForSeconds(3f);
                _renderer.flipX = playerPos.x > pos.x;
                Spawn();
            }
            else
            {
                rigid.MovePosition(rigid.position +
                                   (Vector2)(playerPos - pos).normalized * curSpeed * Time.deltaTime);
                _renderer.flipX = playerPos.x > pos.x;
            }

            yield return new WaitForFixedUpdate();
            rigid.velocity = Vector2.zero;
        }
    }

    public void Spawn()
    {
        GameObject obj = ObjectPooler.Instance.GenerateGameObject(enemyObject);
        obj.transform.position = transform.position;
        obj.transform.Translate(Vector2.one * UnityEngine.Random.Range(-.3f, .3f));
    }
}
