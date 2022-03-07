using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private float bulletSpeed = 1.2f;    // ź�� �ӵ�

    private void Update()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        // ���� ����� bulletSpeed�� ����
        rigidbody2D.velocity = this.transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // bullet�� ���� �ݴ´ٸ�
        if (collision.gameObject.CompareTag("Wall"))
        {
            // ������Ʈ �ı�
            Destroy(gameObject);
        }
    }
}
