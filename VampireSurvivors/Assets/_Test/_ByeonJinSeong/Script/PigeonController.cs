using System.Collections;
using UnityEngine;

public class PigeonController : Item
{
    //public float speed = 2f; // 속도
    public float distance = 1f; // 멀어졌을 때 따라가는 거리       

    // Test : 임시
    [HideInInspector] public float Xdistance = 0.5f;
    [HideInInspector] public float Ydistance = 1f; 
    public float Fspeed = 2f;

    void FixedUpdate()
    {
        MovePigeon();
        InvokeRepeating("EnemySearch", 0f, 0.5f);
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

    // Test 어택 관련 수정 4/5
    public GameObject attackPrefab;
    private GameObject tempPrefab;

    protected override void ActiveAttack(int i)
    {
        if(TempTarget != null)
        {
            Debug.Log($"플레이어 위치 : {player.transform.position}");
            //StartCoroutine(CreateObject());
            tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
            //AttackCurve.Init.myPigeon = this.gameObject;
            //tempPrefab.GetComponent<AttackCurve>().myPigeon = this.gameObject; // 비둘기 연결 
            tempPrefab.transform.position = transform.position; // 초기 위치 지정
            tempPrefab.transform.Translate(Vector2.one * Random.Range(-.1f, .1f)); // 위치 지정
            tempPrefab.GetComponent<AttackCurve>().enemy.transform.position = TempTarget.position;        // 가까운 적 인식
            
            //흠... 이건 어떻게 쓰는걸까?
            //ProjectilePrefab stat = tempPrefab.GetComponent<ProjectilePrefab>(); // 발사체 속도 데미지 지정
            //stat.speed = GetSpeed();
            //stat.amount = GetAmount();
            //stat.penetrate = GetPenetrate();
        }
    }

    // 적 Search 함수용 변수
    public LayerMask LayerMask = 0;     // OverlapSphere 함수 LayerMask "Enemy" Layer를 찾기위한 변수
    public Transform TempTarget = null; // 가까운 적 저장 변수

    public Vector2 size;                 // 공격사정거리
    void EnemySearch()
    {

        // 플레이어 기준 사정거리 안 적을 저장하는 변수
        Collider2D[] cols = Physics2D.OverlapBoxAll(player.transform.position, size, 0, LayerMask);
        Transform ShortTarget = null;     // 가까운적 저장 변수

        // 사정거리안 적이 존재할 경우
        if (cols.Length > 0)
        {
            float ShortDistans = Mathf.Infinity;     // 최초 비교 거리
            foreach (Collider2D c in cols)
            {
                // 거리 비교 함수 distans, magnitude, sqrMagnitude 비교
                // distans, magnitude 두 함수는 정확한 거리를 계산
                // sqrMagnitude 계산된 거리의 제곱을 반환, 루트연산을 하지않아 연산속도가 빠르다.
                // 요약! 정확한 거리를 구할때 distans, magnitude 사용
                // A와 B사이의 특정 거리를 멀다 가깝다로 비교할 경우 SqrMagnitude가 비교적 적합하다.
                float distans = Vector3.SqrMagnitude(transform.position - c.transform.position);
                if (ShortDistans > distans)  // 더 가까운 거리 저장
                {
                    // 가까운 Enemy 갱신
                    ShortDistans = distans;
                    ShortTarget = c.transform;
                }
            }
        }
        TempTarget = ShortTarget;
    }

    protected override void Level2()
    {
        amount++;
    }

    protected override void Level3()
    {
        minMight += 20;
        maxMight += 20;
    }

    protected override void Level4()
    {
        penetrate += 2;
    }

    protected override void Level5()
    {
        amount++;
    }

    protected override void Level6()
    {
        minMight += 20;
        maxMight += 20;
    }

    protected override void Level7()
    {
        penetrate += 2;
    }

    protected override void Level8()
    {
        minMight += 20;
        maxMight += 20;
    }

    // 사거리 시각화용
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(player.transform.position, size);
    }

    /*
     * Rarity    : 확률
     * minmight  : 최소공격력 
     * maxmight  : 최대공격력
     * CoolDown  : 공격속도
     * Area      : 오브젝트 크기
     * Speed     : 투사체 속도
     * Duration  : 지속시간
     * Amount    : 프리팹 개수
     * penetrate : 관통
     */
}