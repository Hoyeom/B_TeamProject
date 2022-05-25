using System;
using Unity.Mathematics;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    internal int penetrate;
    internal float speed;
    internal float might;
    internal float area;

    internal Rigidbody2D rigid;
    public AudioClip shootSoundClip;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        if(shootSoundClip != null)
            Managers.Audio.FXPlayerAudioPlay(shootSoundClip);
    }

    private void OnDisable()
    {
        transform.eulerAngles = Vector3.zero;
    }


    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;

        col.gameObject.GetComponent<Enemy>()?.TakeDamage(might, transform.position);
        if (--penetrate > 0) return;
        ObjectPooler.Instance.DestroyGameObject(gameObject);
    }
}