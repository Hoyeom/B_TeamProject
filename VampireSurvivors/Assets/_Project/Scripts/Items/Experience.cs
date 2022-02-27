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
    private readonly LayerMask _ignoreRay = 2; //IgnoreRay
    private Collider2D _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
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
        gameObject.layer = _ignoreRay;
        float pushPower = 6;
        _player = player.gameObject.GetComponent<Player>();
        rigid.AddForce((transform.position - player.position).normalized * pushPower, ForceMode2D.Impulse);
        StartCoroutine(GoPlayerRoutine(player));
    }
    private IEnumerator GoPlayerRoutine(Transform player)
    {
        float magnetPower = 4;
        
        while (true)
        {
            
            rigid.AddForce((player.position - transform.position).normalized * magnetPower, ForceMode2D.Impulse);
            yield return new WaitForSeconds(.5f);
            rigid.velocity = rigid.velocity / 3;
            _collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player?.AddExp(exp);
            Destroy(gameObject);
        }
          
    }
}
