using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Slider expSlider;
    public Text levelText;

    private void Awake()
    {
        Instance = this;
    }

    public void SetLevelUp(float min, float max, int level = 1)
    {
        levelText.text = $"LV {level}";
        expSlider.minValue = min;
        expSlider.maxValue = max;
    }

    public void SetPickExp(float cur)
    {
        expSlider.value = cur;
    }
}