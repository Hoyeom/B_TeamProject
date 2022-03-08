using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonAttack : MonoBehaviour
{
    public GameObject AttackBullet;    // 공격 매체
    public GameObject Target;          // Test 테스트(임시) 타겟  

    public int Shoot = 10;

    private void Start()
    {
        // 함수 반복 호출
        InvokeRepeating("Attack",2f,1f );
    }

    public void Attack()
    {
        // 코루틴
        StartCoroutine(CreateObject());
    }

    IEnumerator CreateObject()
    {
        int _Shoot = Shoot;
        // 공격 횟수 만큼반복 
        while (_Shoot > 0)
        {
            _Shoot--;
            GameObject attack = Instantiate(AttackBullet, transform); // 프리팹 생성(나중에 )
            attack.GetComponent<AttackCurve>().MyPigeon = gameObject; // 비둘기 연결
            attack.GetComponent<AttackCurve>().enemy = Target;        // 적 (나중에 가까운 적 인식하게)
            yield return new WaitForSeconds(0.1f);                    // 공격 간격
        }
        yield return null;
    }
}
