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

        //IEnumerator enumerator = EnemySpeedSlow(enemy);
        //StartCoroutine(EnemySpeedSlow(enemy));
        //StartCoroutine(enumerator);
        //enumerator.MoveNext();


        if (--penetrate > 0) return;
        ObjectPooler.Instance.DestroyGameObject(gameObject);
    }

    /*IEnumerator EnemySpeedSlow(Enemy enemy)
    {
        Debug.Log("coroutine start" + Time.time);
        enemy.speed = 0.1f;
        yield return new WaitForSecondsRealtime(2.0f);
        enemy.speed = 1f;
        Debug.Log("coroutine end" + Time.time);
    }
    */

}
