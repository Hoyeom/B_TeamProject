using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    #region Test

    private AudioSource _audioSource;
    

    #endregion

    private int i;
    
    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();
    }

    public void AudioPlay(AudioClip clip)
    {
        //AudioSource audio = gameObject.AddComponent<AudioSource>();
        _audioSource.PlayOneShot(clip);
        //audio.clip = clip;
        //audio.volume = 0.2f;
        //audio.Play();
        //Destroy(audio, 1);
    }
    
}
