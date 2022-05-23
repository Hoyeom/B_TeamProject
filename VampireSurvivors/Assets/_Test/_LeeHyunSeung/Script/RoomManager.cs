using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Transform player = null;
    public int stageIndex = 0;
    public int randomStage = 0;
    
    public GameObject[] stage = null;
    public GameObject[] bossStage = null;
    private GameObject addStage = null;

    public int stageEnemyCount = 0;
    public int killMonsterCount = 0;

    private void Awake()
    {
        // 첫번째 맵
        stageIndex++;
        addStage = ObjectPooler.Instance.GenerateGameObject(stage[randomStage]);
    }

    private void Start()
    {
        MonsterCount();
    }

    private void Update()
    {
        if(stageEnemyCount-killMonsterCount == 0)
        {
            OnGet();
        }
    }

    public void NextStage()
    {
        stageIndex++;
        addStage.SetActive(false);
        if (stageIndex%5 != 0)
        {
            // 랜덤으로 일반방 이동
            int random = Random.Range(0, stage.Length);
            randomStage = random;
            addStage = ObjectPooler.Instance.GenerateGameObject(stage[randomStage]);
            PlayerReposion();
            Debug.Log($"{addStage}Stage,{stageIndex % 5}");
            
        }
        else
        {
            // 보스방 순서대로
            Debug.Log($"보스방 입장");
            addStage = ObjectPooler.Instance.GenerateGameObject(bossStage[(stageIndex / 5) - 1]);
            PlayerReposion();
            Debug.Log($"{addStage}Stage,{stageIndex}");
        }
    }

    // 케릭터 이동포인트로 이동
    private void PlayerReposion()
    {
        Transform playerReposion = addStage.transform.Find("PlayerPoint").transform;
        Managers.Game.Player.transform.position = playerReposion.position;
    }

    private void OnGet()
    {
        GameObject gate = addStage.GetComponent<Gate>().gameObject;
        gate.SetActive(true);
        killMonsterCount = 0;
    }

    private int MonsterCount()
    {
        EnemySpawnPoint[] monsterSpwner = addStage.GetComponentsInChildren<EnemySpawnPoint>();
        for(int i =0; i<monsterSpwner.Length; i++)
        {
            stageEnemyCount += monsterSpwner[i].enemyCount;
        }
        return stageEnemyCount;
    }
}
