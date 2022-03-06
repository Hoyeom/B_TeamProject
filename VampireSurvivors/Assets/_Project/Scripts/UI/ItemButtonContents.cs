using System;
using UnityEngine;

public class ItemButtonContents : MonoBehaviour
{
    public AudioClip levelUpSound;
    private void OnEnable()
    {
        AudioManager.Instance.UIAudioPlay(levelUpSound);
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
