using System;
using UnityEngine;

public class ClockPrefab : MonoBehaviour
{
    internal float speed;
    internal float amount;
    internal float might;
    private Rigidbody2D rigid;
    public AudioClip shootSoundClip;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {  
        Managers.Audio.FXPlayerAudioPlay(shootSoundClip);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;

        //if (penetrate-- < 1) return;

        col.gameObject.GetComponent<Enemy>()?.SpeedSlow(0, might);
        //ObjectPooler.Instance.DestroyGameObject(gameObject);
    }
}