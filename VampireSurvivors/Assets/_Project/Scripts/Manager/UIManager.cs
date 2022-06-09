using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    
    [Header("Item")]
    
    private GameObject itemSelectPanel;

    private GameObject itemSelectPanelOrigin;
    public GameObject ItemSelectPanel
    {
        get
        {
            if (itemSelectPanel == null)
            {
                itemSelectPanel = Object.Instantiate(itemSelectPanelOrigin);
                itemButtonContents = itemSelectPanel.transform.GetChild(0).Find("ItemButtonContents");
            }
            return itemSelectPanel;
        }
        set
        {
            itemSelectPanel = value;
        }
    }

    public GameObject itemButton;
    public Transform itemButtonContents;

    private GameObject _damageTextPrefab;

    private GameOverUI _gameOverUI;
    private GameOverUI _gameOverUIOrigin;

    /// <summary>
    /// 필요한 정보를 초기화
    /// </summary>
    public void Initialize()
    {
        _damageTextPrefab = Managers.Resource.Load<GameObject>("UI/DamageText");
        itemButton = Managers.Resource.Load<GameObject>("UI/ItemButton");
        itemSelectPanelOrigin = Managers.Resource.Load<GameObject>("UI/ItemSelectUI");

        _gameOverUIOrigin = Managers.Resource.Load<GameOverUI>("UI/GameOverUI");
    }

    public GameOverUI GetGameOverUI()
    {
        if (_gameOverUI == null)
            _gameOverUI = Object.Instantiate(_gameOverUIOrigin);
        return _gameOverUI;
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