using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonAttack : Item
{
    GameObject myPigeon = null; // 테스트용
    //GameObject Player = null; // 테스트용

    // 적 Search 함수용 변수
    public LayerMask LayerMask = 0;     // OverlapSphere 함수 LayerMask "Enemy" Layer를 찾기위한 변수
    private Transform TempTarget = null; // 가까운 적 저장 변수

    public Vector2 size;                // 공격사정거리

    public GameObject attackPrefab;     // 공격 오브젝트
    private GameObject tempPrefab;      // 생성된 공격 오브젝트
    

    void FixedUpdate()
    {
        InvokeRepeating("EnemySearch", 0f, 1f);
    }

    protected override void ActiveAttack(int i)
    {
        if (TempTarget != null)
        {
            tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
            AttackCurve attackCurve = tempPrefab.GetComponent<AttackCurve>();
            attackCurve.Initialized(myPigeon.transform, TempTarget);
        }
    }

    void EnemySearch()
    {
        // 플레이어 기준 사정거리 안 적을 저장하는 변수
        Collider2D[] cols = Physics2D.OverlapBoxAll(player.transform.position, size, 0, LayerMask);
        Transform ShortTarget = null;     // 가까운적 저장 변수

        // 사정거리안 적이 존재할 경우
        if (cols.Length > 0)
        {
            float ShortDistans = Mathf.Infinity;     // 최초 비교 거리
            foreach (Collider2D col in cols)
            {
                float distans = Vector3.SqrMagnitude(transform.position - col.transform.position);
                if (ShortDistans > distans)  // 더 가까운 거리 저장
                {
                    // 가까운 Enemy 갱신
                    ShortDistans = distans;
                    ShortTarget = col.transform;
                }
            }
        }
        TempTarget = ShortTarget;
    }
    

    protected override void Level1()
    {
        
    }

    protected override void Level2()
    {
        amount++;
    }

    protected override void Level3()
    {
        minMight += 20;
        maxMight += 20;
    }

    protected override void Level4()
    {
        penetrate += 2;
    }

    protected override void Level5()
    {
        amount++;
    }

    protected override void Level6()
    {
        minMight += 20;
        maxMight += 20;
    }

    protected override void Level7()
    {
        penetrate += 2;
    }

    protected override void Level8()
    {
        minMight += 20;
        maxMight += 20;
    }

    protected override void WeaponEquipFX()
    {
        var obj = ObjectPooler.Instance.GenerateGameObject(weaponEquipFx);
        obj.GetComponent<PigeonController>().target = player.transform;
        myPigeon = obj;
    }

    /*
     * Rarity    : 확률
     * minmight  : 최소공격력 
     * maxmight  : 최대공격력
     * CoolDown  : 공격속도
     * Area      : 오브젝트 크기
     * Speed     : 투사체 속도
     * Duration  : 지속시간
     * Amount    : 프리팹 개수
     * penetrate : 관통
     */

    // 사거리 시각화용
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(player.transform.position, size);
    //}
}
