using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCurve : ProjectilePrefab
{
    public float tspeed = 2f;
    public float t = 0;

    // 임시 포인터 변수
    Vector2[] point = new Vector2[4];              // 위치 계산용 4개 포인트 배열
    [HideInInspector] public Transform myPigeon;  // 둘기 위치
    [HideInInspector] public GameObject enemy;     // 적 Prefands 위치 저자용
    public float posX = 3;    // x좌표 생성용
    public float posY = 2;    // y좌표 생성용

    public float CorPosition = 0.5f;  // y축 보정용

    // Bezier Curve 좌표값 불러오기
    private void OnEnable()
    {
        t = 0;
        myPigeon = GameObject.FindWithTag("Bird").transform;
        // P0 -> 시작 위치
        point[0] = myPigeon.transform.position;
        // P1 -> 비둘기 Object의 Point, 1
        point[1] = RandPoint(myPigeon.transform.position);
        // P2 -> 적 Object의 Point, 2
        point[2] = RandPoint(enemy.transform.position); ;
        // P3 -> 적 Object 위치
        point[3] = enemy.transform.position;
        point[3][1] += CorPosition;
    }
    void FixedUpdate()
    {
        if (t > 1)
        {
            ObjectPooler.Instance.DestroyGameObject(gameObject);
            return;
        }

        t += Time.fixedDeltaTime;
        // Debug.Log($"speed {speed}"); 왜 0일까?...
        AttackTrajectroy();
    }

    // Point 좌표 랜덤 지정
    Vector2 RandPoint(Vector2 point)
    {
        float x, y;

        x = posX * Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad) + point.x;
        y = posY * Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad) + point.y;

        return new Vector2(x,y);
    }
    // Bezier Curve 궤적 그리기
    private void AttackTrajectroy()
    {
        float x = BezierPoint(point[0].x, point[1].x, point[2].x, point[3].x);
        float y = BezierPoint(point[0].y, point[1].y, point[2].y, point[3].y);
        transform.position = new Vector2(x,y);
    }

    // Bazier Curve Equation
    // p=(1-t)^3*P0 + 3(1-t)^2*t*P1+3(1-t)t^2*P2+t^3*P3
    private float BezierPoint(float P0, float P1, float P2, float P3)
    {
        return Mathf.Pow((1 - t), 3) * P0
            + Mathf.Pow((1 - t), 2) * 3 * t * P1
            + Mathf.Pow(t, 2) * 3 * (1 - t) * P2
            + Mathf.Pow(t, 3) * P3;
    }

}
