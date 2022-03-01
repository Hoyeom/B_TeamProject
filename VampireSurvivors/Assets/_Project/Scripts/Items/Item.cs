using System;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour,IItem
{
    public enum ItemType
    {
        Active,
        Duration,
        Passive
    }

    public Player player;
    public ItemType itemType;
    
    public int maxLevel;
    public int level;

    public float might;
    public float coolDown;
    public float area;
    public float speed;
    public float duration;
    public int amount;

    private void OnEnable()
    {
        Invoke("ItemActive", 1f);
        //player.playerStatRank.GetMight(might);
        //player.playerStatRank.GetCooldown(coolDown);
        //player.playerStatRank.GetArea(area);
        //player.playerStatRank.GetSpeed(speed);
        //player.playerStatRank.GetDuration(duration);
        //player.playerStatRank.GetAmounts(amount);
    }

    private void ItemActive()
    {
        if (level <= 0) return;
        switch (itemType)
        {
            case ItemType.Active:
                StartCoroutine(ActiveAttackRoutine());
                break;
            case ItemType.Duration:
                StartCoroutine(DurationAttackRoutine());
                break;
            case ItemType.Passive:
                PassiveAttack();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected virtual void ActiveAttack(int i)
    {
        
    }
    protected virtual void DurationAttack(int i)
    {
        
    }
    protected virtual void PassiveAttack()
    {
        
    }
    
    IEnumerator ActiveAttackRoutine()
    {
        // Debug.Log("ActiveAttackRoutine");
        while (true) // 게임 종료 혹은 아이템 제거 까지
        {
            for (int i = 0; i < player.playerStatRank.GetAmounts(amount); i++)
            {
                ActiveAttack(i);
                yield return new WaitForSeconds(.05f);
            }
            yield return new WaitForSeconds(player.playerStatRank.GetCooldown(coolDown));
        }
        yield return null;
    }
    
    IEnumerator DurationAttackRoutine()
    {
        // Debug.Log("DurationAttackRoutine");
        while (true) // 게임 종료 혹은 아이템 제거 까지
        {
            for (int i = 0; i < player.playerStatRank.GetAmounts(amount); i++)
            {
                DurationAttack(i);
                yield return null;
            }
            yield return new WaitForSeconds(player.playerStatRank.GetDuration(duration));
            yield return new WaitForSeconds(player.playerStatRank.GetCooldown(coolDown));
        }
        yield return null;
    }
    
    IEnumerator PassiveAttackRoutine()
    {
        // Debug.Log("PassiveAttackRoutine");
        while (true) // 게임 종료 혹은 아이템 제거 까지
        {
            PassiveAttack();

            yield return new WaitForSeconds(player.playerStatRank.GetCooldown(coolDown));
        }
        yield return null;
    }

    public int GetLevel() => level;
    public bool IsMaxLevel => !(level > maxLevel);

    public ItemType GetItemType() => itemType;
    
    public void LevelUp()
    {
        
    }
}
