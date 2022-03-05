using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject[] instantItems;
    
    private GameObject[] itemObj;
    private Item[] itemScript;
    private int itemLen;

    private void Start()
    {
        itemLen = items.Length;
        itemObj = new GameObject[itemLen];
        itemScript = new Item[itemLen];
        for (int i = 0; i < itemLen; i++)
        {
            itemObj[i] = Instantiate(items[i], transform);
            itemScript[i] = itemObj[i].GetComponent<Item>();
        }
    }

    public void ActiveRandomButton()
    {
        UIManager.Instance.itemSelectPanel.SetActive(true);

        List<int> tempList = GetRandIndex();
        if (tempList.Count == 0)
        {
            for (int i = 0; i < instantItems.Length; i++)
            {
                GameObject button = Instantiate(UIManager.Instance.itemButton, UIManager.Instance.itemButtonContents);
                button.GetComponent<ItemButton>().SetButtonImage(instantItems[i]);
            }
        }
        else
        {
            foreach (var index in tempList)
            {
                GameObject button = Instantiate(UIManager.Instance.itemButton, UIManager.Instance.itemButtonContents);
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