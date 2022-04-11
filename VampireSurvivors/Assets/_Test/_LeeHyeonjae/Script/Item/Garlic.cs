using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garlic : Item
{
    //해야할거
    //마늘 스프라이트 이미지 넣기,레벨당 설정
    //------------------------------------------------
    // float minMight;  // 최소 공격력
   //  float maxMight;  // 최대 공격력
    // float coolDown;  // 쿨타임
   //  float area;      // 범위(크기)
   //-------------------------------------------------
   

    LayerMask mask = new LayerMask();


    protected override void Initialize()
    {
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
        area += 1;
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
        coolDown -= 0.05f;
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
   
        