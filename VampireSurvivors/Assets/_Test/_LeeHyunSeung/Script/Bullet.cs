using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private float bulletSpeed = 1.2f;    // 탄알 속도

    private void Update()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        // 보는 방향대 bulletSpeed로 날라감
        rigidbody2D.velocity = this.transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // bullet이 벽에 닫는다면
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 오브젝트 파괴
            Destroy(gameObject);
        }
    }
}
