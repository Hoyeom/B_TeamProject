using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance;
    private static Managers Instance
    {
        get
        {
            Initialize();
            return instance;
        }
    }

    private static void Initialize()
    {
        
    }

    private AudioManager _audio;
    public AudioManager Audio = Instance._audio;


}
