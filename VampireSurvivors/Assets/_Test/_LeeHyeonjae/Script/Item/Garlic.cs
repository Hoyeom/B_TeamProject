using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garlic : Item
{
    //해야할거
   //레벨당 수치 기제
   //마늘 오라에 애니메이션 넣기??
    
    LayerMask mask = new LayerMask();


    protected override void Initialize()
    {
        base.Initialize();
        mask = LayerMask.GetMask("Enemy");
    }

    protected override void WeaponEquipFX()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    protected override void Level2()
    {
        minMight += 1;
        maxMight += 1;
    }

    protected override void Level3()
    {
        minMight += 1;
        maxMight += 1;
    }

    protected override void Level4()
    {
        coolDown -= 0.05f;
    }

    protected override void Level5()
    {
        area += 0.5f;
        transform.localScale = Vector3.one * (GetArea() * 2.0f);
        Debug.Log(GetArea());
        Debug.Log(transform.localScale);
    }

    protected override void Level6()
    {
        minMight += 1;
        maxMight += 1;
    }

    protected override void Level7()
    {
        minMight += 1;
        maxMight += 1;
    }

    protected override void Level8()
    {
        area += 1;
        transform.localScale = Vector3.one * (GetArea() * 2.0f);
        coolDown -= 0.05f;
        Debug.Log(GetArea());
        Debug.Log(transform.localScale);
    }


    protected override void PassiveAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetArea(), mask); // transform으로 해도 상관없음
        if (colliders != null)
        {
            //데미지 주기
            for (int i = 0; i < colliders.Length; i++)
            {
                try
                {
                    colliders[i].gameObject.GetComponent<Enemy>().TakeDamage(GetMight(), transform.position);
                }
                catch (NullReferenceException e)
                {
                    Debug.Log("qwdqwdwqd");
                }

                // Debug.Log(GetMight());
            }
        }

    }


}
   
        