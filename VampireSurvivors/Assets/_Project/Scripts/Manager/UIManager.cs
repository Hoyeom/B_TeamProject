using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    [Header("Item")]
    public GameObject itemSelectPanel;
    public GameObject itemButton;
    public Transform itemButtonContents;

    private GameObject _damageTextPrefab;

    /// <summary>
    /// 필요한 정보를 초기화
    /// </summary>
    public void Initialize()
    {
        _damageTextPrefab = Managers.Resource.Load<GameObject>("UI/DamageText");
        itemButton = Managers.Resource.Load<GameObject>("UI/ItemButton");
        itemSelectPanel = Managers.Resource.Load<GameObject>("UI/ItemSelectUI");
    
        itemSelectPanel = Object.Instantiate(itemSelectPanel);
        itemButtonContents = itemSelectPanel.transform.GetChild(0).Find("ItemButtonContents");
    }
    
    /// <summary>
    /// 해당 위치에 데미지 텍스트를 생성
    /// </summary>
    /// <param name="damage">피해량</param>
    /// <param name="pos">위치</param>
    public void SpawnDamageText(int damage,Vector3 pos)
    {
        GameObject obj = ObjectPooler.Instance.GenerateGameObject(_damageTextPrefab);
        obj.GetComponent<DamageTextPrefab>().SpawnText(damage, pos);
    }

}