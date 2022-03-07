using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float speed = 2f; // 속도
    public float distance = 1f; // 멀어졌을 때 따라가는 거리       

    public PlayerStatRank PlayerObject; 

    Transform player; // Player         변수이름 변경 부탁드립니다 // Item 상속하시면 player.tranform으로 사용가능합니다.
    Transform Pigeon; // 귀여운 둘기     // Pigeon은 현재 코드상 transform으로 대체 가능합니다.

    // 공격용
    private GameObject tempPrefab;
    public GameObject attackPrefab;


    private void Awake()
    {                                           // 아이템은 캐릭터의 자식 컴포넌트로 제작 예정으로 자동으로 따라가게 됩니다
        PlayerObject = new PlayerStatRank(); // Test : 추 후 플레이어 속도에 맞춰서 이동
        player = GameObject.Find("Player").transform;  
        Pigeon = GetComponent<Transform>();     // Pigeon은 현재 코드상 transform으로 대체 가능합니다.
    }

    void Update()                   // Fixed Update 사용 부탁드립니다.
    {
        MovePigeon();
    }

    // 플레이어 따라가기
    private void MovePigeon()
    {
        if (Vector2.Distance(Pigeon.position, player.position) > distance)
        {
            Pigeon.position = Vector2.MoveTowards(Pigeon.position,
                new Vector2(player.position.x - 0.5f, player.position.y + 1.0f),
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
        // 공격코드 작성전 디스코드 오셔서 말해주세요
        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
    }
}