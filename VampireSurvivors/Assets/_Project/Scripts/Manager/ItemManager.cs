using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class ItemManager
{
    private GameObject[] items;
    private GameObject[] instantItems;

    private Item playerItem;
    private GameObject[] itemObj;
    private Item[] itemScript;
    private int itemLen;


    public void Initialize()
    {
        items = Managers.Resource.LoadAll<GameObject>("Items");
        instantItems = Managers.Resource.LoadAll<GameObject>("Items/Instance");
        
        itemLen = items.Length;
        itemScript = new Item[itemLen];
        itemObj = new GameObject[itemLen];
        
        playerItem = Managers.Game.Player.GetComponentInChildren<Item>();
        
        for (var i = 0; i < items.Length; i++)
        {
            itemObj[i] = Object.Instantiate(items[i], Managers.Instance.transform);
            itemScript[i] = itemObj[i].GetComponent<Item>();
            
            
            if (playerItem == null)  continue;
            if (playerItem.itemId != itemScript[i].itemId) continue;
            
            Object.Destroy(itemObj[i]);
            itemScript[i] = playerItem;
            items[i] = itemObj[i] = playerItem.gameObject;
        }
        
        foreach (var item in instantItems)
            Object.Instantiate(item, Managers.Instance.transform);
    }

    public void GetRandItem()
    {
        Managers.UI.itemSelectPanel.SetActive(true);

        List<int> tempList = GetRandIndex();
        
        if (tempList.Count == 0)
        {
            foreach (var item in instantItems)
            {
                GameObject button = Object.Instantiate(Managers.UI.itemButton, Managers.UI.itemButtonContents) as GameObject;
                button.GetComponent<ItemButton>().SetButtonImage(item);
            }
        }
        else
        {
            foreach (var index in tempList)
            {
                GameObject button = UnityEngine.Object.Instantiate(Managers.UI.itemButton, Managers.UI.itemButtonContents);
                button.GetComponent<ItemButton>().SetButtonImage(itemObj[index]);
            }
        }
    }

    private List<int> GetRandIndex()
    {
        List<int> indexList = new List<int>();

        List<int> tempList = new List<int>();

        List<int> rarityList = new List<int>();
        
        for (int j = 0; j < itemScript.Length; j++)
        {
            rarityList.Add(itemScript[j].rarity);
        }

        for (int i = 0; i < 4; i++)
        {
            float randIndex = 0;
            bool isGetItem = false;
            
            tempList.Clear();
            for (int j = 0; j < itemScript.Length; j++)
            {
                tempList.Add(rarityList[j]);
            }

            for (int j = 0; j < tempList.Count; j++)
            {
                randIndex += tempList[j];
            }

            float randomPoint = Random.value * randIndex;
            for (int j = 0; j < tempList.Count; j++)
            {
                if (randomPoint < tempList[j])
                {
                    isGetItem = true;
                    indexList.Add(j);
                    rarityList[j] = 0;
                    break;
                }
                else
                {
                    randomPoint -= tempList[j];
                }
            }

            if (randIndex == 0)
                break;
            
            if (!isGetItem)
            {
                indexList.Add(tempList.Count - 1);
            }
        }
        return indexList;
    }
}