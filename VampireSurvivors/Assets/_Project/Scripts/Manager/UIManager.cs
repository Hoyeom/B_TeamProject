using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject inGameUI = null;
    
    [Header("LEVEL")]
    public Slider expSlider;
    public Text levelText;
    [Header("Item")]
    public GameObject itemSelectPanel;
    public GameObject itemButton;
    public Transform itemButtonContents;
    [Header("Health")]
    public Slider hpSlider;

    public GameObject damageTextPrefab;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMaxExp(float min, float max, int level = 1)
    {
        
        levelText.text = $"LV {level}";
        expSlider.minValue = min;
        expSlider.maxValue = max;
    }

    public void SetExpValue(float cur)=> expSlider.value = cur;

    public void SetMaxHp(float max)=> hpSlider.maxValue = max;

    public void SetHpValue(float cur) => hpSlider.value = cur;

    public void SpawnDamageText(int damage,Vector3 pos)
    {
        GameObject obj = ObjectPooler.Instance.GenerateGameObject(damageTextPrefab);
        obj.GetComponent<DamageTextPrefab>().SpawnText(damage, pos);
    }

}