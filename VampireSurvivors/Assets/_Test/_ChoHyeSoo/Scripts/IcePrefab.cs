using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePrefab : ProjectilePrefab
{
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;

        //col.gameObject.GetComponent<Enemy>()?.HitEnemy(amount, transform.position);
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        enemy.HitEnemy(amount, transform.position);
        enemy.SpeedSlow();

        if (--penetrate > 0) return;
        ObjectPooler.Instance.DestroyGameObject(gameObject);
    }

}
