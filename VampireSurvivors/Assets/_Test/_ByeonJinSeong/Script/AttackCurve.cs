using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCurve : MonoBehaviour
{
    private float t = 0;
    public float speed = 10;

    // Bezier Curve 좌표값 불러오기
    private void Start()
    {
        // P0 -> 시작 위치
        // P1 -> 각 1
        // P2 -> 각 2
        // P3 -> 도착 위치
    }
    private void Update()
    {
        t += Time.deltaTime * speed;
    }
    // Bezier Curve
    private void ATrajectroy()    // 궤적 함수
    {
        transform.position = new Vector2();   // P0~P4 좌표값 넣기
    }

    // Bazier Curve Equation
    // p=(1-t)^3*P0 + 3(1-t)^2*t*P1+3(1-t)t^2*P2+t^3*P3
    private float BezierPoint(float P0, float P1, float P2, float P3)
    {
        return Mathf.Pow((1 - t), 3) * P0 + Mathf.Pow((1 - t), 2) * 3 * t * P1
            + Mathf.Pow(t, 2) * 3 * (1 - t) * P2
            + Mathf.Pow(t, 3) * P3;
    }
}
