using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Item
{
    public GameObject attackPrefab;
    private GameObject tempPrefab;

    private GameObject obj = null;

    private Transform TempTarget;

    public LayerMask LayerMask = 0;
    public Vector2 size;

    void FixedUpdate()
    {
        InvokeRepeating("EnemySearch", 0f, 1f);

    }

    //protected override void PassiveAttack()
    //{
    //    gameObject.transform.position = player.transform.position;
    //    tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
    //    tempPrefab.transform.position = transform.position; // 초기 위치 지정
    //}


    protected override void ActiveAttack(int i)
    {
        //GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (TempTarget != null)
        {
            //TempTarget = enemy.transform;
            //Debug.Log(TempTarget);
            tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
            gameObject.transform.position = obj.transform.position;
            tempPrefab.transform.position = transform.position; // 초기 위치 지정
            //tempPrefab.transform.Translate(Vector2.one * Random.Range(-.1f, .1f)); // 위치 지정

            Vector2 view = TempTarget.position - tempPrefab.transform.position;
            float angle = Mathf.Atan2(view.y + 0.5f, view.x) * Mathf.Rad2Deg;
            tempPrefab.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            ProjectilePrefab stat = tempPrefab.GetComponent<ProjectilePrefab>(); // 발사체 속도 데미지 지정
            stat.speed = GetSpeed();
            stat.amount = GetAmount();

        }
        //else
        //Debug.Log("목표 없음");
    }

    protected override void WeaponEquipFX()
    {
        // 예제

        // weaponEquipFx = Instantiate(pigeon);    // 원하는 프리펩 저장
        // PigeonScript pigeonScript = weaponEquipFx.GetComponent<PigeonScript>(); // 비둘기 스크립트를 저장할 전역변수에 저장
         obj = ObjectPooler.Instance.GenerateGameObject(weaponEquipFx);
        //obj.transform.position = gameObject.transform.parent.position;
        Debug.Log(obj);
    }

    void EnemySearch()
    {
        // 플레이어 기준 사정거리 안 적을 저장하는 변수
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position, size, 0, LayerMask);
        Transform ShortTarget = null;     // 가까운적 저장 변수

        // 사정거리안 적이 존재할 경우
        if (cols.Length > 0)
        {
            //Debug.Log(cols.Length);
            float ShortDistans = Mathf.Infinity;     // 최초 비교 거리
            foreach (Collider2D col in cols)
            {
                float distans = Vector3.SqrMagnitude(transform.position - col.transform.position);
                if (ShortDistans > distans )  // 더 가까운 거리 저장
                {
                    //Debug.Log(col);
                    // 가까운 Enemy 갱신
                    ShortDistans = distans;
                    ShortTarget = col.transform;
                }
            }
        }
        TempTarget = ShortTarget;
    }

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

    // 사거리 시각화용
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
