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
            //gameObject.transform.LookAt(target.transform);
            //Vector2 direction = new Vector2(
            //        transform.position.x - target.transform.position.x,
            //        transform.position.y - target.transform.position.y);

            // 목표물 추적
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            //Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
            //transform.rotation = rotation;

            //Vector3 myPos = transform.position;
            //Vector3 targetPos = target.transform.position;
            //targetPos.z = myPos.z;

            //Vector3 vectorToTarget = targetPos - myPos;
            //Vector3 quaternionToTarget = Quaternion.Euler(0, 0, axis) * vectorToTarget;

            //Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: quaternionToTarget);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        Destroy(gameObject, 10f);
    }

}
