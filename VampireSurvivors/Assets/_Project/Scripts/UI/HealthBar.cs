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



    private void OnChangeHealth(float cur, float max)
    {
        Debug.Log(cur);
        Debug.Log(max);
        slider.value = cur / max;
    }

}
