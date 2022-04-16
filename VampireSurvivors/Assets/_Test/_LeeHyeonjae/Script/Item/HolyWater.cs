using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : Item
{

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
        tempPrefab.transform.position = new Vector2(transform.position.x + 1, 6f); // 초기 위치 지정
        tempPrefab.transform.Translate(Vector2.one * Random.Range(1f, .4f)); // 위치 지정
        tempPrefab.transform.rotation = player.viewRotation; // 방향 지정


        // ProjectilePrefab stat = tempPrefab.GetComponent<ProjectilePrefab>(); // 발사체 속도 데미지 지정
        HolyWaterPerfab stat = tempPrefab.GetComponent<HolyWaterPerfab>(); // 발사체 속도 데미지 지정
        stat.speed = GetSpeed();
        stat.amount = GetAmount();
        stat.area = GetArea();
        stat.rigid.AddForce((Vector2.down * Random.Range(-.2f, .2f)) * speed, ForceMode2D.Impulse);
        stat.rigid.AddTorque(Random.Range(-90f, 90f));
        //Debug.Log("작동");



    }
}