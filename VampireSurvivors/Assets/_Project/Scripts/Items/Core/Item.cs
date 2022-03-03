using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Item : MonoBehaviour,IItem
{
    public enum ItemType
    {
        Active,
        Duration,
        Passive
    }

    public Player player;
    
    [Header("Item")]
    public ItemType itemType;
    public int maxLevel;
    public int level;
    public int rarity;

    [Header("Status")]
    public float minMight;
    public float maxMight;
    public float coolDown;
    public float area;
    public float speed;
    public float duration;
    public int amount;

    void Awake()
    {
        //player = transform.parent.GetComponent<Player>();
    }

    private void OnEnable()
    {
        if (level > 0)
        {
            Invoke("ItemActive", 1f);   // 임시
        }
            
    }
    public void ItemActive()
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
                StartCoroutine(PassiveAttackRoutine());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    // 상속받아서 바꿔야하는 함수입니다. 바꾸는 것 예제는 Knife.cs 참조
    #region OverrideFunc

    protected virtual void ActiveAttack(int i)
    {
        
    }
    protected virtual void DurationAttack(int i)
    {
        
    }
    protected virtual void PassiveAttack()
    {
        
    }

    #endregion
    
    #region AttackRoutine
    IEnumerator ActiveAttackRoutine()
    {
        // Debug.Log("ActiveAttackRoutine");
        while (true) // 게임 종료 혹은 아이템 제거 까지
        {
            for (int i = 0; i < GetAmount(); i++)
            {
                ActiveAttack(i);
                yield return new WaitForSeconds(.05f);
            }
            yield return new WaitForSeconds(GetCooldown());
        }
    }
    
    IEnumerator DurationAttackRoutine()
    {
        // Debug.Log("DurationAttackRoutine");
        while (true) // 게임 종료 혹은 아이템 제거 까지
        {
            for (int i = 0; i < GetAmount(); i++)
            {
                DurationAttack(i);
                yield return null;
            }
            yield return new WaitForSeconds(GetDuration());
            yield return new WaitForSeconds(GetCooldown());
        }
    }
    
    IEnumerator PassiveAttackRoutine()
    {
        // Debug.Log("PassiveAttackRoutine");
        while (true) // 게임 종료 혹은 아이템 제거 까지
        {
            PassiveAttack();

            yield return new WaitForSeconds(GetCooldown());
        }
    }
    

    #endregion

    #region GetItemInfo

    public int GetLevel() => level;
    public bool IsMaxLevel() => !(level > maxLevel);
    public ItemType GetItemType() => itemType;

    #endregion

    #region StatLoad
    public float GetCooldown() => player.playerStatRank.GetCooldown(coolDown);
    public float GetDuration() => player.playerStatRank.GetDuration(duration);
    public float GetArea() => player.playerStatRank.GetArea(area);
    public float GetSpeed() => player.playerStatRank.GetSpeed(speed);
    public float GetMight() => player.playerStatRank.GetMight(Random.Range(minMight, maxMight));   // min max는 상속받은 후 지정
    public float GetAmount() => player.playerStatRank.GetAmounts(amount);

    #endregion


    public void LevelUp()
    {
        
    }
    
}
