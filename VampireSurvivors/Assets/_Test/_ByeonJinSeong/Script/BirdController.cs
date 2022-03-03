using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float speed = 2f;       // 속도
    public float distance = 1f;    // 멀어졌을 때 따라가는 거리

    public PlayerStatRank PlayerObject;

    Transform player;         // Player
    Transform Pigeon;         // 귀여운 둘기

    // 공격용
    private GameObject tempPrefab;
    public GameObject attackPrefab;


    private void Awake()
    {
        PlayerObject = new PlayerStatRank(); // Test : 추 후 플레이어 속도에 맞춰서 이동
        player = GameObject.Find("Player").transform;
        Pigeon = GetComponent<Transform>();

    }

    void Update()
    {
        MovePigeon();
    }

    // 플레이어 따라가기
    private void MovePigeon()
    {
        if (Vector2.Distance(Pigeon.position, player.position) > distance )
        {
            Pigeon.position = Vector2.MoveTowards(Pigeon.position, 
                new Vector2(player.position.x-0.5f, player.position.y+1.0f),
                speed * Time.deltaTime);
            Direction();
        }
    }

    // 바라 보는 방향 수정
    private void Direction()
    {
        if (Pigeon.position.x - player.position.x < 0)
            Pigeon.eulerAngles = new Vector3(0, 180, 0);
            
        else
            Pigeon.eulerAngles = new Vector3(0, 0, 0);
    }

    // Test : 나중에 따로
    private void PigeonAttack()
    {
        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
    }

}