using System;
using System.Collections;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public float exp;
    private Rigidbody2D rigid;
    private SpriteRenderer _renderer;
    public Sprite[] expSprite;
    private Player _player;
    private readonly LayerMask _ignoreRayer = 2; //IgnoreRay
    private readonly LayerMask _itemLayer = 3; //ItemRay
    private Collider2D _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        gameObject.layer = _itemLayer;
        _collider.isTrigger = false;
    }

    public void DropExp(float dropExp)
    {
        exp = dropExp;
        if (exp >= 100)
            _renderer.sprite = expSprite[3];
        else if (exp >= 60)
            _renderer.sprite = expSprite[2];
        else if (exp >= 40)
            _renderer.sprite = expSprite[1];
        else if (exp >= 20)
            _renderer.sprite = expSprite[0];
    }
    
    
    public void GoPlayer(Transform player)
    {
        gameObject.layer = _ignoreRayer;
        
        _player = player.gameObject.GetComponent<Player>();

        StartCoroutine(PushExp(player.transform.position));
        
        StartCoroutine(PullExp(player));
    }

    IEnumerator PushExp(Vector3 player)
    {
        float pushPower = 6;
        float pushTime = .5f; 

        while (0 < pushTime)
        {
            pushTime -= Time.deltaTime;
            
            //rigid.MovePosition(rigid.position + (Vector2) (transform.position - player) * pushPower * Time.deltaTime);
            
            rigid.MovePosition(rigid.position +
                               (Vector2) (transform.position - player) * pushPower * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }
    
    IEnumerator PullExp(Transform player)
    {
        yield return new WaitForSeconds(.5f);
        _collider.isTrigger = true;
        const float pullPower = 24;

        while (true)
        {
            rigid.MovePosition(rigid.position +
                               -(Vector2) (transform.position - player.transform.position).normalized * pullPower * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player?.AddExp(exp);
            StopAllCoroutines();
            ObjectPooler.Instance.DestroyGameObject(gameObject);
            //Destroy(gameObject);
        }
          
    }
}
