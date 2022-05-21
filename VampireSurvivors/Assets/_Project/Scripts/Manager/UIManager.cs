using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    [Header("Item")]
    public GameObject itemSelectPanel;
    public GameObject itemButton;
    public Transform itemButtonContents;

    private GameObject _damageTextPrefab;

    public void Initialize()
    {
        _damageTextPrefab = Managers.Resource.Load<GameObject>("UI/DamageText");
        itemButton = Managers.Resource.Load<GameObject>("UI/ItemButton");
        itemSelectPanel = Managers.Resource.Load<GameObject>("UI/ItemSelectUI");
    
        itemSelectPanel = Object.Instantiate(itemSelectPanel);
        itemButtonContents = itemSelectPanel.transform.GetChild(0).Find("ItemButtonContents");
    }
    
    public void SpawnDamageText(int damage,Vector3 pos)
    {
        GameObject obj = ObjectPooler.Instance.GenerateGameObject(_damageTextPrefab);
        obj.GetComponent<DamageTextPrefab>().SpawnText(damage, pos);
    }

}