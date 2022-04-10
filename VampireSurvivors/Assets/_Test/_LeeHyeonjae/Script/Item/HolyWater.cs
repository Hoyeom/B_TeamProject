using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
    private float amount = 0.2f;
    private float HolyWaterCreateDelay = 5.0f; // 레벨별로 나중에
    private float HolyWaterAttackDelay = 0.3f; // 레벨별로 나중에
    private float HoleyWaterRange = 4.6f; // 레벨별로 나중에
    private GameObject _player;
    public GameObject Water;
    private Transform WaterStartLine;
    private Rigidbody2D rigid;
    private IEnumerator coroutine;


    private void Start()
    {
        coroutine = holyWater();
        _player = GameObject.FindWithTag("Player");
        //WaterStartLine = _player.transform;
        StartCoroutine(HolyWaterSet());
    }

    private IEnumerator HolyWaterSet()
    {
        while (true)
        {
            yield return new WaitForSeconds(HolyWaterCreateDelay);
            WaterStartLine = _player.transform;
            GameObject instance = Instantiate(Water, WaterStartLine.position, WaterStartLine.rotation);
            Debug.Log(instance.name);
            Destroy(instance, HolyWaterCreateDelay);
            StartCoroutine(coroutine);
        }

    }

    
    private IEnumerator holyWater()
    {
        while (true)
        {
            Vector2 playerPos = _player.transform.position;
            LayerMask mask = LayerMask.GetMask("Enemy");
            Collider2D[] col2d = Physics2D.OverlapCircleAll(playerPos, HoleyWaterRange, mask); // transform으로 해도 상관없음
            if (col2d != null)
            {

                //데미지 주기
                for (int i = 0; i < col2d.Length; i++)
                {
                    col2d[i].gameObject.GetComponent<Enemy>().HitEnemy(amount, transform.position);
                    Debug.Log("데미지 작동");

                }
                yield return new WaitForSeconds(HolyWaterAttackDelay);
                
            }
            
        }
    }

}


