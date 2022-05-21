using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    #region Test

    public AudioSource bgm_Audio;

    public AudioSource fx_PlayerAudio;
    private float fx_PlayerSoundTimer;
    
    public AudioSource fx_EnemyAudio;
    private float fx_EnemySoundTimer;

    public AudioSource ui_Audio;
    #endregion

    private void Awake()
    {
        Instance = this;
        bgm_Audio = gameObject.AddComponent<AudioSource>();
        fx_PlayerAudio = gameObject.AddComponent<AudioSource>();
        fx_EnemyAudio = gameObject.AddComponent<AudioSource>();
        ui_Audio = gameObject.AddComponent<AudioSource>();

        StartCoroutine(FXPlayerTimer());
        StartCoroutine(FXEnemyTimer());
    }
    public void FXPlayerAudioPlay(AudioClip clip)
    {
        if (clip == null) return;

        if (fx_PlayerSoundTimer > 0)
            return;
        fx_PlayerAudio.PlayOneShot(clip);
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
        if (clip == null) return;

        if (fx_EnemySoundTimer > 0)
            return;
        fx_EnemyAudio.PlayOneShot(clip);
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
        ui_Audio.PlayOneShot(clip);
    }

    public void SetUIVolume(float volume)
    {
        ui_Audio.volume = volume;
    }



}
