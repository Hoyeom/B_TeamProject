using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Item
{
    public GameObject attackPrefab;
    private GameObject tempPrefab;

    private Transform TempTarget;

    //void FixedUpdate()
    //{
    //    InvokeRepeating("EnemySearch", 0f, 1f);
       
    //}

    protected override void ActiveAttack(int i)
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            TempTarget = enemy.transform;
            Debug.Log(TempTarget);
            tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
            tempPrefab.transform.position = transform.position; // 초기 위치 지정
            tempPrefab.transform.Translate(Vector2.one * Random.Range(-.1f, .1f)); // 위치 지정
            //tempPrefab.GetComponent<BulletProjectile>().target.transform.position = TempTarget.position;

            // 방향 지정
            //tempPrefab.transform.rotation = player.viewRotation; 
            //tempPrefab.transform.rotation = Quaternion.LookRotation(target.position);
            //gameObject.transform.LookAt(target);

            ProjectilePrefab stat = tempPrefab.GetComponent<ProjectilePrefab>(); // 발사체 속도 데미지 지정
            stat.speed = GetSpeed();
            stat.amount = GetAmount();
        }
        
    }

    //void EnemySearch()
    //{
    //    // 플레이어 기준 사정거리 안 적을 저장하는 변수
    //    Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position, size, 0, LayerMask);
    //    Transform ShortTarget = null;     // 가까운적 저장 변수

    //    // 사정거리안 적이 존재할 경우
    //    if (cols.Length > 0)
    //    {
    //        float ShortDistans = Mathf.Infinity;     // 최초 비교 거리
    //        foreach (Collider2D col in cols)
    //        {
    //            float distans = Vector3.SqrMagnitude(transform.position - col.transform.position);
    //            if (ShortDistans > distans)  // 더 가까운 거리 저장
    //            {
    //                // 가까운 Enemy 갱신
    //                ShortDistans = distans;
    //                ShortTarget = col.transform;
    //            }
    //        }
    //    }
    //    target = ShortTarget;
    //}

    // 레벨의 따른 증가량
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
        coolDown -= 0.3f;
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
        coolDown -= 0.2f;
    }

    
}
