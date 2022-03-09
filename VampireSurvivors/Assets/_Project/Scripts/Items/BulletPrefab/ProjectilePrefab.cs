using System;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    internal int penetrate;
    internal float speed;
    internal float amount;
    public AudioClip shootSoundClip;

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

        col.gameObject.GetComponent<Enemy>()?.HitEnemy(amount, transform.position);
        if (--penetrate > 0) return;
        ObjectPooler.Instance.DestroyGameObject(gameObject);
    }
}