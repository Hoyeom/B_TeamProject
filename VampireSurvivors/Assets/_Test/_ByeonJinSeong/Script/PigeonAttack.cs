using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonAttack : MonoBehaviour
{
    public GameObject AttackBullet;    // 공격 매체
    public GameObject Target;          // Test 테스트(임시) 타겟  

    public int Shoot = 10;
    bool tr = true;
    //private void Update()
    //{
    //    if (tr)
    //    {
    //        int _Shoot = Shoot;
    //        while (_Shoot > 0)
    //        {
    //            Debug.Log("생성 while문 입장");
    //            _Shoot--;
    //            GameObject attack = Instantiate(AttackBullet, transform);
    //            attack.GetComponent<AttackCurve>().MyPigeon = gameObject;
    //            attack.GetComponent<AttackCurve>().enemy = Target;
    //        }
    //        tr = false;
    //    }
    //}

    private void Start()
    {
        InvokeRepeating("Attack",2f,1f );
        //Attack();
    }

    public void Attack()
    {
        StartCoroutine(CreateObject());
    }

    IEnumerator CreateObject()
    {
        Debug.Log("코루틴 실행");
        int _Shoot = Shoot;
        while (_Shoot > 0)
        {
            Debug.Log("생성 while문 입장");
            _Shoot--;
            GameObject attack = Instantiate(AttackBullet, transform);
            attack.GetComponent<AttackCurve>().MyPigeon = gameObject;
            attack.GetComponent<AttackCurve>().enemy = Target;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
