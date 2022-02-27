using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class ActiveItem : Item
{
    public float damage;
    public float coolDown;
    public float speed;
    public GameObject projectile;
    private void Start()
    {
        itemType = ItemType.Active;
        // player = transform.parent.GetComponent<Player>();
        coolDown = player.playerStatRank.GetCooldown(coolDown);
        
        StartCoroutine(Attack());

    }

    protected virtual IEnumerator Attack()
    {
        yield return null;
    }
    
    
}
