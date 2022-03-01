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
        //tempPrefab = Instantiate(attackPrefab);
        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        tempPrefab.transform.position = transform.position;
        tempPrefab.transform.Translate(Vector2.one * Random.Range(-.2f,.2f));
        tempPrefab.transform.rotation = player.viewRotation;

        tempPrefab.GetComponent<ProjectilePrefab>().speed = player.playerStatRank.GetSpeed(speed);
        tempPrefab.GetComponent<ProjectilePrefab>().amount = player.playerStatRank.GetMight(might);
        //Destroy(tempPrefab, 5);
    }
}
