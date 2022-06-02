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
    private GameObject bossMonster = null;
    GameObject gate = null;

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
        ExpOff();
        gate = addStage.transform.Find("Gate").gameObject;
        gate.SetActive(false);
        addStage.SetActive(false);
        if (stageIndex%5 != 0)
        {
            // 랜덤으로 일반방 이동
            int random = Random.Range(0, stage.Length);
            randomStage = random;
            addStage = ObjectPooler.Instance.GenerateGameObject(stage[randomStage]);
            gate = addStage.transform.Find("Gate").gameObject;
            gate.SetActive(false);
            PlayerReposion();
            //Debug.Log($"{addStage}Stage,{stageIndex % 5}");
        }
        else
        {
            // 보스방 순서대로
            Debug.Log($"보스방 입장");
            addStage = ObjectPooler.Instance.GenerateGameObject(bossStage[(stageIndex / 5) - 1]);
            PlayerReposion();
            bossMonster = addStage.GetComponentInChildren<FMonster>().gameObject;
            //Debug.Log($"{addStage}Stage,{stageIndex}");
        }
        MonsterCount();
    }

    // 케릭터 이동포인트로 이동
    private void PlayerReposion()
    {
        Transform playerReposion = addStage.transform.Find("PlayerPoint").transform;
        Managers.Game.Player.transform.position = playerReposion.position;
    }

    private void OnGet()
    {
        //GameObject gate = addStage.GetComponentInChildren<Gate>().gameObject;
        gate = addStage.transform.Find("Gate").gameObject;
        gate.SetActive(true);
        stageEnemyCount = 0;
        killMonsterCount = 0;
    }

    private int MonsterCount()
    {
        EnemySpawnPoint[] monsterSpwner = addStage.GetComponentsInChildren<EnemySpawnPoint>();
        if(bossMonster != null)
        {
            stageEnemyCount++;
        }
        else
        {
            for (int i = 0; i < monsterSpwner.Length; i++)
            {
                GameObject spawnObj = monsterSpwner[i].gameObject;
                Debug.Log(monsterSpwner[i].gameObject.name);
                spawnObj.SetActive(true);
                stageEnemyCount += monsterSpwner[i].enemyCount;
            }
        }
       
        return stageEnemyCount;
    }

    private void ExpOff()
    {
        GameObject[] exp = GameObject.FindGameObjectsWithTag("Exp");
        for (int i = 0; i < exp.Length; i++)
        {
            Destroy(exp[i]);
        }
    }
}
