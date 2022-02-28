using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Item
{
    public GameObject attackPrefab;
    private GameObject tempPrefab;
    
    protected override void ActiveAttack(int i)
    {
        // Debug.Log("Attack");
        tempPrefab = Instantiate(attackPrefab);
        tempPrefab.transform.position = transform.position + new Vector3(Random.Range(0, .5f), Random.Range(0, .5f));
        tempPrefab.transform.rotation = player.viewRotation;

        tempPrefab.GetComponent<ProjectilePrefab>().speed = player.playerStatRank.GetSpeed(speed);
        tempPrefab.GetComponent<ProjectilePrefab>().amount = player.playerStatRank.GetMight(might);
        Destroy(tempPrefab, 5);
    }
}
