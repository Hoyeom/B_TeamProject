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
    
    internal Player player;

    public GameObject weaponEquipFx;
    
    [Header("UI")]
    public Sprite spriteImg;
    public string itemName;
    public string[] description = new string[8];

    [Header("Item")] public ItemType itemType;
    public int itemId;  // 같은 아이템 인지 확인용
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

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        ItemActive();   
    }

    // 아이템 획득시 사용되는 함수
    public void ItemActive()
    {
        if (level <= 0) return;
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
        WeaponEquipFX();
    }

    // 상속받아서 바꿔야하는 함수입니다. 바꾸는 것 예제는 Knife.cs 참조

    #region OverrideFunc

    // 쿨타임을 마다 개수(amount) 만큼 반복 후 지속시간이 끝난 후 쿨타임 시작
    protected virtual void ActiveAttack(int i)
    {
    }
    
    // 획득후 쿨타임 마다 1회 호출
    protected virtual void PassiveAttack()
    {
    }

    // 1회 사용
    protected virtual void InstantItemActive()
    {
        
    }
    
    // 무기 획득시 효과
    protected virtual void WeaponEquipFX()
    {
        // 예제
        
        // weaponEquipFx = Instantiate(pigeon);    // 원하는 프리펩 저장
        // PigeonScript pigeonScript = weaponEquipFx.GetComponent<PigeonScript>(); // 비둘기 스크립트를 저장할 전역변수에 저장
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

    public int GetLevel() => level; // 아이템의 현재 레벨을 받아온다
    internal bool IsMaxLevel() => (level > maxLevel - 1);   // 아이템이 최대 레벨인지 확인
    internal bool IsMaxLevel(int level) => (level > maxLevel - 1);  // 캐릭터 레벨업 후 선택창에서 다음 레벨이 최대인지 확인
    public ItemType GetItemType() => itemType;  // 아이템 종류

    #endregion

    #region StatLoad
    internal float GetCooldown() => player.playerStatRank.GetCooldown(coolDown);
    internal float GetDuration() => player.playerStatRank.GetDuration(duration);
    internal float GetArea() => player.playerStatRank.GetArea(area);
    internal float GetSpeed() => player.playerStatRank.GetSpeed(speed);
    internal float GetMight() => player.playerStatRank.GetMight(Random.Range(minMight, maxMight)); // min max는 상속받은 후 지정
    internal float GetAmount() => player.playerStatRank.GetAmounts(amount);
    internal int GetPenetrate() => penetrate;
    
    #endregion

    #region LevelUp

    // 아이템 획득시 호출되는 함수
    public void EnableItem()  
    {
        if (itemType == ItemType.Instant)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            InstantItemActive();
            return;
        }
        // Jinseong Test용
        // if(itemName.Equals("비둘기"))
        // {
        //     Debug.Log($"테스트 용입니다 : {itemName}");
        //     GameObject.Find("Nest").transform.GetChild(0).gameObject.SetActive(true);
        //     player = GameObject.FindWithTag("Player").GetComponent<Player>();
        // }
        // ^^^^^Test용
        transform.position = player.transform.position;
        LevelUpItem();
    }

    // 레벨 별 효과를 적용 함수
    public void LevelUpItem()
    {
        if(IsMaxLevel()) return;
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
    protected virtual void Level1() { }
    protected virtual void Level2() { }
    protected virtual void Level3() { }
    protected virtual void Level4() { }
    protected virtual void Level5() { }
    protected virtual void Level6() { }
    protected virtual void Level7() { }
    protected virtual void Level8() { }


    #endregion

    // public으로 작성된 아이템 설명을 가져온다
    internal string GetDescription() => description[level];
}