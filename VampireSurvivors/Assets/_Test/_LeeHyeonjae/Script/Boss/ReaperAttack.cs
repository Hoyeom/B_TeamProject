using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperAttack : MonoBehaviour
{
    private float speed = 250f;
    float movement= 2f;
    private Rigidbody2D rigid;
    private Player _player;
    private float might = 10.0f;
    Vector2 pos;
    private Vector3 playerPos;
    private Vector3 dir;
    private Vector3 debug;



    private void OnEnable()
    {
        debug = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height));
        rigid = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<Player>();
        playerPos = _player.transform.position + new Vector3(0,0.5f);
        dir = playerPos - transform.position;
        dir = dir.normalized;
        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)));
        Debug.DrawRay(transform.position,debug-transform.position,Color.green,1);
        //Debug.DrawRay(transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)) - transform.position,Color.green,1);
    }


    private void FixedUpdate()
    {
       
        transform.Translate(dir * movement *Time.fixedDeltaTime , Space.World);
        transform.Rotate(0, 0, -Time.fixedDeltaTime * speed);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            other.gameObject.GetComponent<Player>().AttackChangeHealth(might);
            Debug.Log("닿았어");
            ObjectPooler.Instance.DestroyGameObject(gameObject);
        }
    }

}
