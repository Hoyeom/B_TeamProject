using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Old : MonoBehaviour
{
    private Rigidbody2D rigid;
    private float bulletSpeed = 2.0f;    // 탄알 속도

    private void Update()
    {
        rigid = GetComponent<Rigidbody2D>();
        // 보는 방향대 bulletSpeed로 날라감
        rigid.velocity = this.transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // bullet이 벽에 닫는다면
        if (collision.CompareTag("Enemy"))
        {
            // 오브젝트 파괴
            Destroy(gameObject);
        }
    }
}
