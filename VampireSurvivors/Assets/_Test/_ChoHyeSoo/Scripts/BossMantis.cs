using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Project.Scripts.Player;


public class BossMantis : MonoBehaviour
{
    private Player _player;
    private Rigidbody2D rigid;
    public GameObject enemyObject;
    public AudioClip hitSoundClip;
    public GameObject expPrefab;
    private SpriteRenderer _renderer;
    public LayerMask targetLayer;
    private Animator _animator;


    //private Animator anim = null;

    private readonly int hashHitAnim = Animator.StringToHash("hitTrigger");
    public float speed;
    public float maxHealth;
    public int fire_rate;  //프레임기준 공격딜레이
    public float attackRadius;
    public float dropExp;



    private int shoot_time;
    private float curSpeed;
    private float health;
    private bool resurrectCheck;
    private float damage;
    private int rushTime;
    private int resurrectAmount;
    private int rushSpeed;



    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        //anim = GetComponent<Animator>();

        curSpeed = speed;
        health = maxHealth;
        shoot_time = fire_rate;
        resurrectCheck = false;
        damage = 1;
        resurrectAmount = 10;
        rushSpeed = 10;

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
           
                rushTime = 0;
                while (rushTime < 10)
                {
                    //몇초동안 어느방향으로 하는
 
                    rigid.MovePosition(rigid.position +
                                       (Vector2)(playerPos - pos).normalized * curSpeed * rushSpeed * Time.deltaTime);
                    yield return new WaitForFixedUpdate();

                    rushTime++;

                }

                //yield return new WaitForSeconds(3f);
                _renderer.flipX = playerPos.x > pos.x;
                Spawn();
            }
            else
            {
                
                rigid.MovePosition(rigid.position +
                                    (Vector2)(playerPos - pos).normalized * curSpeed * Time.deltaTime);
                _renderer.flipX = playerPos.x > pos.x;
            }

            Attack();

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

    public void Spawn()
    {
        GameObject obj = ObjectPooler.Instance.GenerateGameObject(enemyObject);
        obj.transform.position = transform.position;
        obj.transform.Translate(Vector2.one * UnityEngine.Random.Range(-.3f, .3f));
    }


    public void TakeDamage(float damage, Vector2 target)
    {
        if (health < 1)
        { return; }

        UIManager.Instance.SpawnDamageText((int)damage, transform.position);
        health -= damage;

        if(resurrectCheck == false && health < 1)
        {
            resurrectCheck = true;
            health = resurrectAmount;
            curSpeed = resurrectAmount;
            damage = resurrectAmount;
        }

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
        _animator.SetTrigger(hashHitAnim);
    }
}
