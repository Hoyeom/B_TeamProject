using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider = null;
    Player player = null;
    private void Awake()
    {
        slider = transform.GetComponentInChildren<Slider>();
        player = transform.root.GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.OnChangeHealth += OnChangeHealth;
    }

    private void OnDisable()
    {
        player.OnChangeHealth -= OnChangeHealth;
    }

    
    /// <summary>
    /// 슬라이더 값을 0~1 사이 값으로 변경
    /// </summary>
    /// <param name="cur">현재 체력</param>
    /// <param name="max">최대 체력</param>
    private void OnChangeHealth(float cur, float max)
    {
        slider.value = cur / max;
    }

}
