using UnityEngine;

public class PigeonController : Item
{
    //public float speed = 2f; // 속도
    public float distance = 1f; // 멀어졌을 때 따라가는 거리       

    // Test : 임시
    public float Xdistance = 0.5f; 
    public float Ydistance = 1f; 
    public float Fspeed = 1f;

    public PlayerStatRank PlayerObject; 

    //Transform player; // Player         변수이름 변경 부탁드립니다 // Item 상속하시면 player.tranform으로 사용가능합니다.

    // 공격용
    private GameObject tempPrefab;
    public GameObject attackPrefab;


    void FixedUpdate()                   // Fixed Update 사용 부탁드립니다.
    {
        MovePigeon();
    }

    // 플레이어 따라가기
    private void MovePigeon()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > distance)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(player.transform.position.x - Xdistance, player.transform.position.y + Ydistance),Fspeed*Time.deltaTime);
            Direction();
        }
    }

    // 바라 보는 방향 수정
    private void Direction()
    {
        if (transform.position.x - player.transform.position.x < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Test : 나중에 따로
    private void PigeonAttack()
    {
        // 공격코드 작성전 디스코드 오셔서 말해주세요
        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
    }
}