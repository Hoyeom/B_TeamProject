using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // 오브젝트 파괴
            Destroy(gameObject);
        }
    }
}
