using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandumRoom : MonoBehaviour
{
    public Transform player = null;
    public int stageIndex = 0;
    public int randomStage = 0;
    public GameObject[] stage = null;
    public GameObject[] bossStage = null;
    private GameObject addObject = null;

    private void Awake()
    {
        stageIndex++;
        addObject = ObjectPooler.Instance.GenerateGameObject(stage[randomStage]);
        //(GameObject)Instantiate(stage[0], Vector3.zero, Quaternion.identity);
    }

    public void NextStage()
    {
        stageIndex++;
        //Destroy(addObject);
        addObject.SetActive(false);
        if (stageIndex%5 != 0)
        {
            int random = Random.Range(0, stage.Length);
            randomStage = random;
            addObject = ObjectPooler.Instance.GenerateGameObject(stage[randomStage]);
            //(GameObject)Instantiate(stage[randomStage], Vector3.zero, Quaternion.identity);
            PlayerReposion();
            Debug.Log($"{addObject}Stage,{stageIndex % 5}");
            
        }
        else
        {
            Debug.Log($"보스방 입장");
            addObject = ObjectPooler.Instance.GenerateGameObject(bossStage[(stageIndex / 5) - 1]);
            //(GameObject)Instantiate(bossStage[(stageIndex / 5 )- 1], Vector3.zero, Quaternion.identity);
            PlayerReposion();
            Debug.Log($"{addObject}Stage,{stageIndex}");
        }
    }

    private void PlayerReposion()
    {
        //Debug.Log("실행");
        Transform playerReposion = addObject.GetComponentInChildren<PlayerPoint>().transform;
        //Debug.Log(playerReposion.position);
        player.position = playerReposion.position;
    }
}
