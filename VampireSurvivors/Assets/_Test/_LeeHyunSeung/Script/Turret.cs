using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret :Item
{
    public GameObject attackPrefab;
    private GameObject tempPrefab;

    protected override void ActiveAttack(int i)
    {
        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        tempPrefab.transform.position = transform.position; // 초기 위치 지정
        tempPrefab.transform.rotation = player.viewRotation; // 방향 지정

        ProjectilePrefab stat = tempPrefab.GetComponent<ProjectilePrefab>(); // 발사체 속도 데미지 지정
        stat.speed = GetSpeed();
        stat.amount = GetAmount();
        stat.penetrate = GetPenetrate();
    }

    protected override void Level2()
    {
        minMight += 3;
        maxMight += 3;
    }

    protected override void Level3()
    {
        amount++;
        minMight += 3;
        maxMight += 3;

    }

    protected override void Level4()
    {
        minMight += 3;
        maxMight += 3;
    }

    protected override void Level5()
    {
        coolDown -= 0.05f;
    }

    protected override void Level6()
    {
        minMight += 3;
        maxMight += 3;
    }

    protected override void Level7()
    {
        amount++;
        minMight += 3;
        maxMight += 3;
    }

    protected override void Level8()
    {
        coolDown -= 0.05f;
    }

    
    public GameObject bulletObj;         // 탄알 오브젝트
    public float bulletRate = 2f;       // 탄알 딜레이
    private float timeRate = 1.5f;      // 최근 발사한 시간

    private Transform target;           // 발사할 대상


    private void Update()
    {

        Fire();
    }

    private void Fire()
    {
        // timeRate  갱신
        timeRate += Time.deltaTime;

        if (timeRate >= bulletRate)
        {
            // 누적 시간 리셋
            timeRate = 0f;

            target = GameObject.FindGameObjectWithTag("Enemy").transform;   // 발사할 대상의 위치
                                                                            //BulletOj 의 복제본을 transfor.position 위치와 transfor.rotation 회전으로 생성
            GameObject bullet = Instantiate(bulletObj, transform.position, target.transform.rotation);
            //생성된 bullet 게임오브젝트의 정면 방향이 target을 향하도록 회전
            bullet.transform.LookAt(target);
        }
        //Destroy(gameObject, 10f);
    }
    
}
