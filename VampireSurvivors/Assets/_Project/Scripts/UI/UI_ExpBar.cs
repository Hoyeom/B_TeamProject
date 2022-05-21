using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class UI_ExpBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _text;
        
        private void OnEnable()
        {
            Managers.Game.Player.OnChangeExp += ChangeExp;
            Managers.Game.Player.OnChangeLevel += ChangeLevel;
        }

        private void ChangeExp(float cur, float max)
        {
            _slider.value = max / cur;
        }

        private void ChangeLevel(int level)
        {
            _text.text = $"LV {level.ToString()}";
        }
        
    }
}