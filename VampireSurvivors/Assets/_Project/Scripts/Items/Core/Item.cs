using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Instant,
        Active,
        Passive
    }


    /// <summary>
    /// 게임내 있는 플레이어에게 접근
    /// </summary>
    internal Player Player => Managers.Game.Player;

    public GameObject weaponEquipFx;

    [Header("UI")] public Sprite spriteImg;
    public string itemName;
    public string[] description = new string[8];

    [Header("Item")] public ItemType itemType;
    public int itemId; // 같은 아이템 인지 확인용
    public int maxLevel;
    public int level;
    public int rarity;

    [Header("Status")] public float minMight; // 최소 공격력
    public float maxMight; // 최대 공격력
    public float coolDown; // 쿨타임
    public float area; // 범위(크기)
    public float speed; // 투사체 속도
    public float duration; // 지속시간
    public int amount; // 개수
    public int penetrate; // 관통 (투사체에만)
    public float attackInterval; // 발사 간격

    private void Start()
    {
        Initialize();
        ItemActive();
    }

    /// <summary>
    /// 아이템이 플레이어 오브젝트인지 확인하여 레벨 결정
    /// </summary>
    protected virtual void Initialize()
    {
        if (transform.parent == Managers.Game.Player.transform)
            level = 1;
    }

    /// <summary>
    /// 아이템 획득시 공격방식 지정
    /// </summary>
    public void ItemActive()
    {
        if (level <= 0) return;
        WeaponEquipFX();
        switch (itemType)
        {
            case ItemType.Instant:
                InstantItemActive();
                break;
            case ItemType.Active:
                StartCoroutine(ActiveAttackRoutine());
                break;
            case ItemType.Passive:
                StartCoroutine(PassiveAttackRoutine());
                break;
        }
    } 

    // 상속받아서 바꿔야하는 함수입니다. 바꾸는 것 예제는 Knife.cs 참조

    #region OverrideFunc

    /// <summary>
    /// 쿨타임을 마다 개수(amount) 만큼 반복 후 지속시간이 끝난 후 쿨타임 시작
    /// </summary>
    /// <param name="i">반복 횟수</param>
    protected virtual void ActiveAttack(int i)
    {
    }

    /// <summary>
    /// 쿨타임 마다 1회 호출
    /// </summary>
    protected virtual void PassiveAttack()
    {
    }

    /// <summary>
    /// 1회 사용
    /// </summary>
    protected virtual void InstantItemActive()
    {
    }

    /// <summary>
    /// 무기 활성화된 이후 효과 (이펙트)
    /// </summary>
    protected virtual void WeaponEquipFX()
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
                if (attackInterval > 0)
                    yield return new WaitForSeconds(attackInterval);
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

    
    /// <summary>
    /// 아이템의 현재 레벨
    /// </summary>
    public int GetLevel() => level; // 아이템의 현재 레벨을 받아온다
    /// <summary>
    /// 아이템이 최대 레벨인지 확인
    /// </summary>
    internal bool IsMaxLevel() => (level > maxLevel - 1);

    /// <summary>
    /// 다음 레벨이 최대 레벨인지 확인
    /// </summary>
    internal bool IsMaxNextLevel() => (level + 1 > maxLevel - 1); // 캐릭터 레벨업 후 선택창에서 다음 레벨이 최대인지 확인
    /// <summary>
    /// 아이템 타입 확인
    /// </summary>
    public ItemType GetItemType() => itemType; // 아이템 종류

    #endregion

    #region StatLoad

    internal float GetCooldown() => Player.playerStatRank.GetCooldown(coolDown);
    internal float GetDuration() => Player.playerStatRank.GetDuration(duration);
    internal float GetArea() => Player.playerStatRank.GetArea(area);
    internal float GetSpeed() => Player.playerStatRank.GetSpeed(speed);
    /// <returns>최소 공격력과 최대 공격력 사이 값</returns>
    internal float GetMight() => Player.playerStatRank.GetMight(Random.Range(minMight, maxMight)); // min max는 상속받은 후 지정
    internal float GetAmount() => Player.playerStatRank.GetAmounts(amount);
    internal int GetPenetrate() => penetrate;

    #endregion

    #region LevelUp

    /// <summary>
    /// 아이템 획득시 호출
    /// </summary>
    public void EnableItem()
    {
        if (itemType == ItemType.Instant)
        {
            InstantItemActive();
            return;
        }
        transform.position = Player.transform.position;
        LevelUpItem();
    }

    /// <summary>
    /// 레벨별 효과 적용
    /// </summary>
    public void LevelUpItem()
    {
        if (IsMaxLevel()) return;
        switch (++level)
        {
            case 1:
                ItemActive();
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
                rarity = 0;
                Level8();
                break;
        }
    }

    #endregion

    #region LevelOverride

    // 아이템에서 레벨별 효과를 정의하는 Overriding 함수 
    protected virtual void Level1()
    {
    }

    protected virtual void Level2()
    {
    }

    protected virtual void Level3()
    {
    }

    protected virtual void Level4()
    {
    }

    protected virtual void Level5()
    {
    }

    protected virtual void Level6()
    {
    }

    protected virtual void Level7()
    {
    }

    protected virtual void Level8()
    {
    }

    #endregion

   
    /// <summary>
    /// 아이템 설명 Get
    /// </summary>
    /// <returns>프리펩에 설정된 레벨에 알맞은 설멍</returns>
    internal string GetDescription() => description[level];
}