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
        
        _player = player.gameObject.GetComponent<Player>();

        StartCoroutine(PushExp(player.transform.position));
        
        StartCoroutine(PullExp(player));
    }

    IEnumerator PushExp(Vector3 player)
    {
        float pushPower = Random.Range(1f, 2f);
        float pushTime = 1;

        while (0 < pushTime)
        {
            pushTime -= Time.deltaTime;
            
            rigid.MovePosition(rigid.position +
                               (Vector2) (transform.position - player) * pushPower * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }
    
    IEnumerator PullExp(Transform player)
    {
        yield return new WaitForSeconds(1);
        _collider.isTrigger = true;
        const float pullPower = 12;

        while (true)
        {
            rigid.MovePosition(rigid.position +
                               -(Vector2) (transform.position - player.transform.position).normalized * pullPower * Time.deltaTime);
            yield return null;
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
