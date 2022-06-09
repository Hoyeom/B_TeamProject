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

        /// <summary>
        /// 슬라이더 값을 0~1 사이 값으로 변경
        /// </summary>
        /// <param name="cur">현재 경험치</param>
        /// <param name="max">최대 경험치</param>
        private void ChangeExp(float cur, float max)
        {
            _slider.value = cur / max;
        }
        
        /// <summary>
        /// 레벨 변경시 설정
        /// </summary>
        private void ChangeLevel(int level)
        {
            _text.text = $"LV {level.ToString()}";
        }
        
    }
}