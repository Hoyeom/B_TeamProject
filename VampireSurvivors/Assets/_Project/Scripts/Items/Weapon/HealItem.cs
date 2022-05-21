using UnityEngine;

public class HealItem : Item
{

    [SerializeField] private float healAmount = 5;
    protected override void InstantItemActive()
    {
        Player.HealPlayer(healAmount);
    }
}