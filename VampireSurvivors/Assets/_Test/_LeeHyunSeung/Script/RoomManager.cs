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
    public bool playerMove = false;

    public int stageEnemyCount = 0;
    public int killMonsterCount = 0;
    public int totalEnemyCount = 0;
    public float playTime = 0.0f;
    private GameObject switchObj = null;

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
        playTime += Time.deltaTime;
        if (switchObj != null)
        {
            if (stageEnemyCount - killMonsterCount == 0 && switchObj.activeSelf ==false)
            {

                OnGet();
            }
        }
        else
        {
            if (stageEnemyCount - killMonsterCount <= 0)
            {

                OnGet();
            }
        }
    }

    public void NextStage()
    {
        stageIndex++;
        ExpOff();
        gate = addStage.transform.Find("Gate").gameObject;
        gate.SetActive(false);
        switchObj = null;
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
            if(addStage.GetComponentInChildren<Switch>() != null)
            {
                switchObj = addStage.GetComponentInChildren<Switch>().gameObject;
            }          
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
        playerMove = true;
        Debug.Log(playerMove);
        Transform playerReposion = addStage.transform.Find("PlayerPoint").transform;
        Managers.Game.Player.transform.position = playerReposion.position;
    }

    private void OnGet()
    {
        //GameObject gate = addStage.GetComponentInChildren<Gate>().gameObject;
        gate = addStage.transform.Find("Gate").gameObject;
        gate.SetActive(true);
        totalEnemyCount = totalEnemyCount + stageEnemyCount;
        stageEnemyCount = 0;
        killMonsterCount = 0;
    }

    public int MonsterCount()
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
