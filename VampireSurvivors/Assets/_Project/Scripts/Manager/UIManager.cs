using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [Header("LEVEL")]
    public Slider expSlider;
    public Text levelText;
    [Header("Item")]
    public GameObject itemSelectPanel;
    public GameObject itemButton;
    public Transform itemButtonContents;
    [Header("Health")]
    public Slider hpSlider;
    private void Awake()
    {
        Instance = this;
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


}