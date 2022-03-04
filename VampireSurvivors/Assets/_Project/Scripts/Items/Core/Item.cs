using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour, IItem
{
    public enum ItemType
    {
        Active,
        Duration,
        Passive
    }
    
    public Player player;
    
    [Header("UI")]
    public Sprite spriteImg;
    public string itemName;

    [Header("Item")] public ItemType itemType;
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
    public int penetrate; // 관통 (투사체에만)
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
    public bool IsMaxLevel() => (level > maxLevel - 1);
    public ItemType GetItemType() => itemType;

    #endregion

    #region StatLoad
    public float GetCooldown() => player.playerStatRank.GetCooldown(coolDown);
    public float GetDuration() => player.playerStatRank.GetDuration(duration);
    public float GetArea() => player.playerStatRank.GetArea(area);
    public float GetSpeed() => player.playerStatRank.GetSpeed(speed);
    public float GetMight() => player.playerStatRank.GetMight(Random.Range(minMight, maxMight)); // min max는 상속받은 후 지정
    public float GetAmount() => player.playerStatRank.GetAmounts(amount);
    public int GetPenetrate() => penetrate;
    
    #endregion

    #region LevelUp

    public void EnableItem()
    {
        player = transform.parent.GetComponent<Player>();
        transform.position = player.transform.position;
        LevelUpItem();
    }

    public void LevelUpItem()
    {
        Time.timeScale = 1;
        switch (++level)
        {
            case 1:
                Level1();
                break;
            case 2:
                Level2();
                break;
            case 3:
                Level3();
                break;
            case 4:
                Level4();
                break;
            case 5:
                Level5();
                break;
            case 6:
                Level6();
                break;
            case 7:
                Level7();
                break;
            case 8:
                Level8();
                break;
        }
    }

    #endregion

    #region LevelOverride

    protected virtual void Level1() { ItemActive(); }
    protected virtual void Level2() { }
    protected virtual void Level3() { }
    protected virtual void Level4() { }
    protected virtual void Level5() { }
    protected virtual void Level6() { }
    protected virtual void Level7() { }
    protected virtual void Level8() { }


    #endregion
    
    public virtual string GetDescription()
    {
        string description = null;
        switch (level+1)
        {
            case 1:
                description = "획득";
                break;
            case 2:
                description = "투사체 1 증가";
                break;
            case 3:
                description = "투사체 1 증가, 데미지 5 증가";
                break;
            case 4:
                description = "투사체 1 증가";
                break;
            case 5:
                description = "적 1체 관통";
                break;
            case 6:
                description = "투사체 1 증가";
                break;
            case 7:
                description = "투사체 1 증가, 데미지 5 증가";
                break;
            case 8:
                description = "적 1체 관통";
                break;
        }

        return description;
    }
}