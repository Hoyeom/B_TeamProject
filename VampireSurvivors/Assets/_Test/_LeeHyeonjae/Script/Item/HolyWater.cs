using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : Item
{

    //해야할거
    
    //아이템 상세 수치 설명란에 기입 (데미지 등)

    //-------------------------------

    /* minMight;  // 최소 공격력
     maxMight;  // 최대 공격력
     coolDown;  // 쿨타임
     area;      // 범위(크기)
     speed;     // 투사체 속도
     duration;  // 지속시간
     amount;      // 개수
     penetrate; // 관통 (투사체에만)*/



    public GameObject attackPrefab;
    private GameObject tempPrefab;


    protected override void ActiveAttack(int i)
    {
        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
       // tempPrefab.transform.position = new Vector2(transform.position.x + 1, 6f); // 초기 위치 지정
        tempPrefab.transform.position = new Vector2(transform.position.x + Random.Range(-3f,3f), 6f); // 초기 위치 지정
        tempPrefab.transform.Translate(Vector2.one * Random.Range(1f, .4f)); // 위치 지정
        tempPrefab.transform.rotation = player.viewRotation; // 방향 지정


        HolyWaterPerfab stat = tempPrefab.GetComponent<HolyWaterPerfab>(); // 발사체 속도 데미지 지정
        stat.speed = GetSpeed();
        stat.might = GetMight();
        stat.area = GetArea();
        stat.rigid.AddForce((Vector2.down * Random.Range(-.2f, .2f)) * speed, ForceMode2D.Impulse);
        stat.rigid.AddTorque(Random.Range(-360f, 360f));
        //Debug.Log("작동");

    }
    protected override void Level2()
    {
        minMight += 1;
        maxMight += 1;
    }

    protected override void Level3()
    {
        amount++;
        minMight += 1;
        maxMight += 1;
    }

    protected override void Level4()
    {
        coolDown -= 0.5f;
    }

    protected override void Level5()
    {
        area+=0.25f;
        amount++;
    }

    protected override void Level6()
    {
        amount++;
    }

    protected override void Level7()
    {
        coolDown -= 0.5f;
        minMight += 1;
        maxMight += 1;
    }

    protected override void Level8()
    {
        amount++;
        area += 0.25f;
       
    }

}
