using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float speed = 2f;       // �ӵ�
    public float distance = 1f;    // �־����� �� ���󰡴� �Ÿ�

    public PlayerStatRank PlayerObject;

    Transform player;         // Player
    Transform Pigeon;         // �Ϳ��� �ѱ�

    // ���ݿ�
    private GameObject tempPrefab;
    public GameObject attackPrefab;


    private void Awake()
    {
        PlayerObject = new PlayerStatRank(); // Test : �� �� �÷��̾� �ӵ��� ���缭 �̵�
        player = GameObject.Find("Player").transform;
        Pigeon = GetComponent<Transform>();

    }

        void Update()
    {
        MovePigeon();
    }

    // �÷��̾� ���󰡱�
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

    // �ٶ� ���� ���� ����
    private void Direction()
    {
        if (Pigeon.position.x - player.position.x < 0)
            Pigeon.eulerAngles = new Vector3(0, 180, 0);
            
        else
            Pigeon.eulerAngles = new Vector3(0, 0, 0);
    }

    // Test : ���߿� ����
    private void PigeonAttack()
    {
        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
    }

}
