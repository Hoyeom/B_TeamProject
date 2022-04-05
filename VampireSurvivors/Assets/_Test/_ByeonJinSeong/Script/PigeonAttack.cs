using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonAttack : MonoBehaviour
{
    public GameObject AttackBullet;    // 공격 매체

    // 적 Search 함수용 변수
    public LayerMask LayerMask = 0;     // OverlapSphere 함수 LayerMask "Enemy" Layer를 찾기위한 변수
    public Transform TempTarget = null; // 가까운 적 저장 변수

    public Vector2 size;                 // 공격사정거리
    public int Shoot = 3;                // 공격 횟수
    public float AttackSpeed = 3f;       // 공격 속도

    private void Start()
    {
        // 함수 반복 호출
        //InvokeRepeating("Attack",0f,0.2f );
        //InvokeRepeating("EnemySearch",0f,0.5f );
    }

    public void Attack()
    {
        // 코루틴
        StartCoroutine(CreateObject());
    }

    IEnumerator CreateObject()
    {
        int _Shoot = Shoot;

        // 근처에 적이있는지 여부 확인
        if(TempTarget != null)
        {
            // 공격 횟수 만큼반복 
            while (_Shoot > 0)
            {
                _Shoot--;
                GameObject attack = Instantiate(AttackBullet, transform); // 프리팹 생성(나중에 )
                //attack.GetComponent<AttackCurve>().MyPigeon = gameObject; // 비둘기 연결
                attack.GetComponent<AttackCurve>().enemy.transform.position = TempTarget.position;        // 가까운 적 인식
                yield return new WaitForSeconds(AttackSpeed);                    // 공격 간격
            }
        }
        yield return null;
    }

    void EnemySearch()
    {
        GameObject PlayerPosition = GameObject.FindWithTag("Player");   // 플레이어 기준 가까운적 겨냥

        Collider2D[] cols = Physics2D.OverlapBoxAll(PlayerPosition.transform.position, size, 0, LayerMask); // 사정거리 안 적을 저장하는 변수
        Transform ShortTarget = null;     // 가까운적 저장 변수

        // 사정거리안 적이 존재할 경우
        if(cols.Length > 0)
        {
            float ShortDistans = Mathf.Infinity;     // 최초 비교 거리
            foreach(Collider2D c in cols)
            {
                // 거리 비교 함수 distans, magnitude, sqrMagnitude 비교
                // distans, magnitude 두 함수는 정확한 거리를 계산
                // sqrMagnitude 계산된 거리의 제곱을 반환, 루트연산을 하지않아 연산속도가 빠르다.
                // 요약! 정확한 거리를 구할때 distans, magnitude 사용
                // A와 B사이의 특정 거리를 멀다 가깝다로 비교할 경우 SqrMagnitude가 비교적 적합하다.
                float distans = Vector3.SqrMagnitude(transform.position - c.transform.position);
                if(ShortDistans > distans)  // 더 가까운 거리 저장
                {
                    // 가까운 Enemy 갱신
                    ShortDistans = distans;      
                    ShortTarget = c.transform;
                }
            }
        }
        TempTarget = ShortTarget;
    }

    // 사거리 시각화용
     void OnDrawGizmos()
    {
        GameObject PlayerPosition = GameObject.FindWithTag("Player");
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(PlayerPosition.transform.position, size);
    }
}
