using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    #region Test

    private AudioSource bgm_Audio;

    private AudioSource fx_PlayerAudio;

    private AudioSource fx_EnemyAudio;

    private AudioSource ui_Audio;
    #endregion

    /// <summary>
    /// 오디오 소스 추가
    /// </summary>
    public void Initialize()
    {
        bgm_Audio = Managers.Instance.gameObject.AddComponent<AudioSource>();
        bgm_Audio.loop = true;
        
        fx_PlayerAudio = Managers.Instance.gameObject.AddComponent<AudioSource>();
        fx_EnemyAudio = Managers.Instance.gameObject.AddComponent<AudioSource>();
        ui_Audio = Managers.Instance.gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// 배경 음악 재생
    /// </summary>
    public void BgmAudioPlay(AudioClip clip)
    {
        if(clip == null) return;

        bgm_Audio.clip = clip;
        bgm_Audio.Play();
    }
    
    /// <summary>
    /// 플레이어 효과음 재생
    /// </summary>
    public void FXPlayerAudioPlay(AudioClip clip)
    {
        if (clip == null) return;
        
        fx_PlayerAudio.PlayOneShot(clip);
    }

    /// <summary>
    /// 적의 효과음 재생
    /// </summary>
    public void FXEnemyAudioPlay(AudioClip clip)
    {
        if (clip == null) return;
        fx_EnemyAudio.PlayOneShot(clip);
    }

    /// <summary>
    /// UI 효과음 재생
    /// </summary>
    public void UIAudioPlay(AudioClip clip)
    {
        if (clip == null) return;
        ui_Audio.PlayOneShot(clip);
    }

    /// <summary>
    /// 볼륨 설정 예제 (현승님 아래 코드 보고 만드시면 됩니다 )
    /// </summary>
    public void SetUIVolume(float volume)
    {
        ui_Audio.volume = volume;
    }
    
    public void SetFxVolume(float volume)
    {
        fx_EnemyAudio.volume = volume;
        fx_PlayerAudio.volume = volume;
    }
    
    public void SetBgmVolume(float volume)
    {
        bgm_Audio.volume = volume;
    }

}
