using System.Collections;
using UnityEngine;

public class PigeonController : MonoBehaviour
{
    [Range(0f, 2f)] public float distance = 1f; // 멀어졌을 때 따라가는 거리
    [Range(0.5f, 2f)] public float smoothTime = 1f; // 따라가는 속도
    public Transform target = null;

    Vector2 velo = Vector2.zero;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        MovePigeon();
    }

    // 플레이어 따라가기
    private void MovePigeon()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > distance)
        {
            Vector2 pos = target.transform.position;
            transform.position = Vector2.SmoothDamp(transform.position, pos, ref velo, smoothTime);
            Direction();
        }
    }

    // 바라 보는 방향 수정
    private void Direction()
    {
        if (transform.position.x - target.transform.position.x < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
}