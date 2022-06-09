using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Option : MonoBehaviour
{
    [SerializeField] private TMP_Text fxVolumeText;
    [SerializeField] private TMP_Text bgmVolumeText;
    [SerializeField] private TMP_Text uiVolumeText;

    public void OnChangeFxVolume(float value)
    {
        fxVolumeText.text = $"{value * 100:0}";
        Managers.Audio.SetFxVolume(value);
    }
    
    public void OnChangeBgmVolume(float value)
    {
        bgmVolumeText.text = $"{value * 100:0}";
        Managers.Audio.SetBgmVolume(value);
    }
    
    public void OnChangeUIVolume(float value)
    {
        uiVolumeText.text = $"{value * 100:0}";
        Managers.Audio.SetUIVolume(value);
    }
}
