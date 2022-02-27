using UnityEngine;

public class Item : MonoBehaviour,IItem
{
    public enum ItemType
    {
        Active,
        Passive
    }

    public Player player;
    public ItemType itemType;
    
    public int maxLevel;
    public int level;

    public int GetLevel() => level;
    public bool IsMaxLevel => !(level > maxLevel);

    public ItemType GetItemType() => itemType;
    
    public void LevelUp()
    {
        
    }

    public void PickUp()
    {
        if (level < maxLevel)
        {
            LevelUp();
        }
    }
    

}
