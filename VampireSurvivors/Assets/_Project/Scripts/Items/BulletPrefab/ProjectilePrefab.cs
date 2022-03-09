using System;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    internal int penetrate;
    internal float speed;
    internal float amount;
    internal float area;

    internal Rigidbody2D rigid;
    public AudioClip shootSoundClip;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        AudioManager.Instance.FXPlayerAudioPlay(shootSoundClip);
    }
    

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;

        col.gameObject.GetComponent<Enemy>()?.HitEnemy(amount, transform.position);
        if (--penetrate > 0) return;
        ObjectPooler.Instance.DestroyGameObject(gameObject);
    }
}