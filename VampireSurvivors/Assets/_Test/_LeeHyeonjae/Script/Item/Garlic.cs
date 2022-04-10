using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garlic : Item
{
   
    LayerMask mask = new LayerMask();


    private void Start()
    {
        mask = LayerMask.GetMask("Enemy");
    }


    protected override void PassiveAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetArea(), mask); // transform으로 해도 상관없음
        Debug.Log("디버그");
        if (colliders != null)
        {

            //데미지 주기
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].gameObject.GetComponent<Enemy>().HitEnemy(GetMight(), transform.position);
                Debug.Log(GetMight());
            }
        }

    }


}
   
        