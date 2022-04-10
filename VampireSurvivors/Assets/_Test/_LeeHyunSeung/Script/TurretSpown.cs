using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpown : Item
{
    public GameObject attackPrefab;
    private GameObject tempPrefab;

    //protected override void PassiveAttack()
    //{
    //    tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
    //    tempPrefab.transform.position = player.transform.position; // 초기 위치 지정
    //    tempPrefab.transform.Translate(Vector2.zero ); // 위치 지정   
    //}
}
