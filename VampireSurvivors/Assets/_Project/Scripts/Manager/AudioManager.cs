using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    #region Test

    public AudioSource bgm_Audio;

    public AudioSource fx_PlayerAudio;
    private float fx_PlayerSoundTimer;
    
    public AudioSource fx_EnemyAudio;
    private float fx_EnemySoundTimer;

    public AudioSource ui_Audio;
    #endregion

    public void Initialize()
    {
        bgm_Audio = Managers.Instance.gameObject.AddComponent<AudioSource>();
        bgm_Audio.loop = true;
        
        fx_PlayerAudio = Managers.Instance.gameObject.AddComponent<AudioSource>();
        fx_EnemyAudio = Managers.Instance.gameObject.AddComponent<AudioSource>();
        ui_Audio = Managers.Instance.gameObject.AddComponent<AudioSource>();
    }

    public void BgmAudioPlay(AudioClip clip)
    {
        if(clip == null) return;

        bgm_Audio.clip = clip;
        bgm_Audio.Play();
    }
    
    public void FXPlayerAudioPlay(AudioClip clip)
    {
        if (clip == null) return;
        
        fx_PlayerAudio.PlayOneShot(clip);
    }

    public void FXEnemyAudioPlay(AudioClip clip)
    {
        if (clip == null) return;
        fx_EnemyAudio.PlayOneShot(clip);
    }

    public void UIAudioPlay(AudioClip clip)
    {
        if (clip == null) return;
        ui_Audio.PlayOneShot(clip);
    }

    public void SetUIVolume(float volume)
    {
        ui_Audio.volume = volume;
    }

}
