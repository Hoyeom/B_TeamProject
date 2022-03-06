using System;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    internal int penetrate;
    internal float speed;
    internal float amount;
    private Rigidbody2D rigid;
    public AudioClip shootSoundClip;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        AudioManager.Instance.FXPlayerAudioPlay(shootSoundClip);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;

        if (penetrate-- < 1) return;

        col.gameObject.GetComponent<Enemy>()?.HitEnemy(amount, transform.position);
        ObjectPooler.Instance.DestroyGameObject(gameObject);
    }
}