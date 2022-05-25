using UnityEngine;

public class HealItem : Item
{

    [SerializeField] private float healAmount = 5;
    
    /// <summary>
    /// 회복
    /// </summary>
    protected override void InstantItemActive()
    {
        Player.HealPlayer(healAmount);
    }
}