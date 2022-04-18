using System;
using UnityEngine;

public class BiblePrefab : MonoBehaviour
{
    internal float speed;
    internal float amount;
    internal float duration;
    internal float coolDown;
    internal float area;
    public AudioClip shootSoundClip;
    Transform target;

    private void Start()
    {
        target = GameObject.FindObjectOfType<Player>().transform;
    }

    private void OnEnable()
    {
        AudioManager.Instance.FXPlayerAudioPlay(shootSoundClip);
    }

    private void OnDisable()
    {
        transform.position = Vector3.zero;
    }

    private void FixedUpdate()
    {
        transform.RotateAround(target.position, Vector3.forward, 90f * speed*Time.fixedDeltaTime);
        
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

        col.gameObject.GetComponent<Enemy>()?.TakeDamage(amount, transform.position);
        //ObjectPooler.Instance.DestroyGameObject(gameObject);
    }
}