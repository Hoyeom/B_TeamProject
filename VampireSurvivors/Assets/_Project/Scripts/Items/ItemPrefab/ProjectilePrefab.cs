using System;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    public float speed;
    public float amount;
    public AudioClip shootSoundClip;
    private int penetrate = 1;

    private void Start()
    {
        AudioManager.instance.AudioPlay(shootSoundClip);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;

        if (penetrate-- < 1) return;

        col.gameObject.GetComponent<Enemy>()?.HitEnemy(amount);
        Destroy(gameObject);
    }
}
