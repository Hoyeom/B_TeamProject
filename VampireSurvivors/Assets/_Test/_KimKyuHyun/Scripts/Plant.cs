using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private Player _player;
    private SpriteRenderer _renderer;

    public GameObject attackPrefab;

    public int fire_rate;
    private int shoot_time;
    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Attack());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Attack()
    {
        while(true)
        {
            Vector2 pos = transform.position;
            Vector2 playerPos = _player.transform.position;
            _renderer.flipX = playerPos.x > pos.x;

            shoot_time++;
            if (shoot_time % fire_rate == 0)
            {
                yield return new WaitForSeconds(2f);
                Fire();
            }
        }
    }

    private void Fire()
    {
        GameObject seed = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        seed.transform.position = transform.position;

        Vector2 pos = _player.transform.position - seed.transform.position;
        float rad = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        
        seed.transform.rotation = Quaternion.Euler(0, 0, rad);
    }
}
