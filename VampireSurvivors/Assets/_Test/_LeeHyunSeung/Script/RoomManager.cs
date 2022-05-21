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
    private GameObject addObject = null;

    private void Awake()
    {
        // 첫번째 맵
        stageIndex++;
        addObject = ObjectPooler.Instance.GenerateGameObject(stage[randomStage]);
    }

    public void NextStage()
    {
        stageIndex++;
        addObject.SetActive(false);
        if (stageIndex%5 != 0)
        {
            // 랜덤으로 일반방 이동
            int random = Random.Range(0, stage.Length);
            randomStage = random;
            addObject = ObjectPooler.Instance.GenerateGameObject(stage[randomStage]);
            PlayerReposion();
            Debug.Log($"{addObject}Stage,{stageIndex % 5}");
            
        }
        else
        {
            // 보스방 순서대로
            Debug.Log($"보스방 입장");
            addObject = ObjectPooler.Instance.GenerateGameObject(bossStage[(stageIndex / 5) - 1]);
            PlayerReposion();
            Debug.Log($"{addObject}Stage,{stageIndex}");
        }
    }

    // 케릭터 이동포인트로 이동
    private void PlayerReposion()
    {
        Transform playerReposion = addObject.GetComponentInChildren<PlayerPoint>().transform;
        Managers.Game.Player.transform.position = playerReposion.position;
    }
}
