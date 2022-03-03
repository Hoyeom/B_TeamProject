using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    private GameObject[] itemObj;
    private Item[] itemScript;
    private int[] itemIndex;
    private int itemLen;

    private void Start()
    {
        itemLen = items.Length;
        itemObj = new GameObject[itemLen];
        itemScript = new Item[itemLen];
        for (int i = 0; i < itemLen; i++)
        {
            itemObj[i] = ObjectPooler.Instance.GenerateGameObject(items[0]);
            itemScript[i] = itemObj[i].GetComponent<Item>();
        }
    }

    private GameObject GetRandItem()
    {
        int[] randomList = new int[itemLen];

        for (int i = 0; i < itemLen; i++)
        {
            if (itemScript[i].IsMaxLevel()) continue;
            randomList[i] = itemScript[i].rarity;
        }

        int index = RandomIndex(randomList);

        return items[index];
    }

    private int RandomIndex(int[] probs)
    {
        float index = 0;

        foreach (float prob in probs)
            index += prob;

        float randomPoint = Random.value * index;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }

        return itemLen - 1;
    }
}