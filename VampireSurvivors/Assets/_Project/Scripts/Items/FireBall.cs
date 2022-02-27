using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : ActiveItem
{
    
    
    protected override IEnumerator Attack()
    {
        while (true)
        {
            GameObject attackPrefab = Instantiate(projectile);
            attackPrefab.transform.position = transform.position;
            attackPrefab.transform.rotation = player.viewRotation;
            attackPrefab.GetComponent<ProjectilePrefab>().speed = player.playerStatRank.GetSpeed(speed);
            attackPrefab.GetComponent<ProjectilePrefab>().damage = player.playerStatRank.GetMight(damage);
            attackPrefab.SetActive(true);

            yield return new WaitForSeconds(coolDown);
        }
    }
}
