using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpSnake : MonoBehaviour
{
    private float speed = 10f;
    private Vector3 target;
    bool first;
    public GameObject gas;

    private void Start()
    {
        target = Managers.Game.Player.gameObject.transform.position;

        Vector2 pos = transform.position - BossMonsterMgr.Inst._player.transform.position;
        float radian = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, radian);
        Destroy(this.gameObject, 5f);
    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);

        float distans = Vector3.SqrMagnitude(transform.position - target);
        if (distans < 1f)
        {
            if (!first)
            {
                Instantiate(gas);
                Destroy(this.gameObject);
            }
        }
    }
}
