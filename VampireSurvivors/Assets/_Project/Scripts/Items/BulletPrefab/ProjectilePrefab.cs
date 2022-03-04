using System;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    public int penetrate;
    public float speed;
    public float amount;
    public AudioClip shootSoundClip;

    private void OnEnable()
    {
        AudioManager.Instance.AudioPlay(shootSoundClip);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;

        if (penetrate-- < 1) return;

        col.gameObject.GetComponent<Enemy>()?.HitEnemy(amount, transform.position);
        ObjectPooler.Instance.DestroyGameObject(gameObject);
    }
}