using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Old : MonoBehaviour
{
    public GameObject bulletOj;         // 탄알 오브젝트
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
            GameObject bullet = Instantiate(bulletOj, transform.position, target.transform.rotation);
            //생성된 bullet 게임오브젝트의 정면 방향이 target을 향하도록 회전
            bullet.transform.LookAt(target);
        }
        Destroy(gameObject, 10f);
    }
}
