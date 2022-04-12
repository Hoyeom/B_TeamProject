using System;
using UnityEngine;

public class BiblePrefab : MonoBehaviour
{
    internal float speed;
    internal float amount;
    internal float duration;
    internal float coolDown;
    internal float area;
    private Rigidbody2D rigid;
    public AudioClip shootSoundClip;
    Vector3 offSet;
    Transform target;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = GameObject.FindObjectOfType<Player>().transform;
        offSet = transform.position - target.position;
    }

    private void OnEnable()
    {
        AudioManager.Instance.FXPlayerAudioPlay(shootSoundClip);
    }

    private void FixedUpdate()
    {
        transform.position = target.position + offSet;
        transform.RotateAround(target.position, Vector3.forward, 90f * speed*Time.fixedDeltaTime);

        offSet = transform.position - target.position;
        
        if(Time.timeSinceLevelLoad % (coolDown+duration)<duration)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;

        //if (penetrate-- < 1) return;

        col.gameObject.GetComponent<Enemy>()?.HitEnemy(amount, transform.position);
        //ObjectPooler.Instance.DestroyGameObject(gameObject);
    }
}