using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garlic : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private GameObject _enemy;
    private GameObject _player;
    private float amount = 0.0f;
    private float GarlicDelay = 0.3f; // 레벨별로 나중에
    private float GarlicRange = 4.6f; // 레벨별로 나중에




    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Aura");
        StartCoroutine(Garlics());
    }

    private IEnumerator Garlics()
    {
        while (true)
        {
            Vector2 playerPos = _player.transform.position;
            LayerMask mask = LayerMask.GetMask("Enemy");
            Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPos, GarlicRange, mask); // transform으로 해도 상관없음
            if (colliders != null)
            {
                
                //데미지 주기
                for(int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].gameObject.GetComponent<Enemy>().HitEnemy(amount,transform.position);
                    Debug.Log(amount);
                }
                yield return new WaitForSeconds(GarlicDelay);
                Debug.Log(GarlicDelay);
                
            }
            
        }
    }
}
   
        