using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCurve : MonoBehaviour
{
    private float t = 0;
    public float speed = 0.1f;

    // 임시 포인터 변수
    Vector2[] point = new Vector2[4];
    public GameObject MyPigeon;
    public GameObject enemy;
    public float posX = 0.55f;    // x좌표 생성용
    public float posY = 0.45f;    // y좌표 생성용

    bool hit = false;             // 적을 맞췄는지 여부
    

    // Bezier Curve 좌표값 불러오기
    private void Start()
    {
        // Test 나중에 배열로 바꾸기
        // P0 -> 시작 위치
        point[0] = MyPigeon.transform.position;
        // P1 -> 각 1
        point[1] = RandPoint(transform.position);
        // P2 -> 각 2
        point[2] = RandPoint(enemy.transform.position); ;
        // P3 -> 도착 위치
        point[3] = enemy.transform.position;
    }
    void FixedUpdate()
    {
        if (t > 1) return;
        if (hit) return;

        t += Time.deltaTime * speed;
        AttackTrajectroy();
    }

    Vector2 RandPoint(Vector2 point)
    {
        float x, y;

        x = posX * Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad) + point.x;
        y = posY * Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad) + point.y;

        return new Vector2(x,y);
    }
    // Bezier Curve
    private void AttackTrajectroy()    // 궤적 함수
    {
        transform.position = new Vector2(BezierPoint(point[0].x, point[1].x, point[2].x, point[3].x),
            BezierPoint(point[0].y, point[1].y, point[2].y, point[3].y));
    }

    // Bazier Curve Equation
    // p=(1-t)^3*P0 + 3(1-t)^2*t*P1+3(1-t)t^2*P2+t^3*P3
    private float BezierPoint(float P0, float P1, float P2, float P3)
    {
        return Mathf.Pow((1 - t), 3) * P0 + Mathf.Pow((1 - t), 2) * 3 * t * P1
            + Mathf.Pow(t, 2) * 3 * (1 - t) * P2
            + Mathf.Pow(t, 3) * P3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == enemy)
        {
            hit = true;
            Destroy(gameObject,0.1f);
        }
    }
}
