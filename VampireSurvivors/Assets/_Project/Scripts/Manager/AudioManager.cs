using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    #region Test

    public AudioSource fx_PlayerAudioSource;
    private float fx_PlayerSoundTimer;
    
    public AudioSource fx_EnemyAudioSource;
    private float fx_EnemySoundTimer;

    public AudioSource ui_AudioSource;
    #endregion

    private void Awake()
    {
        Instance = this;
        StartCoroutine(FXPlayerTimer());
        StartCoroutine(FXEnemyTimer());
    }
    public void FXPlayerAudioPlay(AudioClip clip)
    {
        if (fx_PlayerSoundTimer > 0)
            return;
        fx_PlayerAudioSource.PlayOneShot(clip);
        fx_PlayerSoundTimer = 0.01f;
    }
    IEnumerator FXPlayerTimer()
    {
        while (true)
        {
            fx_PlayerSoundTimer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    
    public void FXEnemyAudioPlay(AudioClip clip)
    {
        if (fx_EnemySoundTimer > 0)
            return;
        fx_EnemyAudioSource.PlayOneShot(clip);
        fx_EnemySoundTimer = 0.01f;
    }
    IEnumerator FXEnemyTimer()
    {
        while (true)
        {
            fx_EnemySoundTimer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }




    
    public void UIAudioPlay(AudioClip clip)
    {
        ui_AudioSource.PlayOneShot(clip);
    }

}
