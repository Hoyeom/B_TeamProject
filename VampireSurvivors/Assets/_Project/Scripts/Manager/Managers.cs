using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    public static Managers Instance
    {
        get
        {
            Initialize();
            return _instance;
        }
    }

    private AudioManager _audio = new AudioManager();
    private GameManager _game = new GameManager();
    private ResourceManager _resource = new ResourceManager();
    private ItemManager _item = new ItemManager();
    private UIManager _ui = new UIManager();
    
    public static AudioManager Audio => Instance._audio;
    public static GameManager Game => Instance._game;
    public static ResourceManager Resource => Instance._resource;
    public static ItemManager Item => Instance._item;
    public static UIManager UI => Instance._ui;
    

    private const string DEFAULT_NAME = "@Managers";
    
    private void Awake()
        => gameObject.name = DEFAULT_NAME;
    
    private void Start()
        => Initialize();

    
    /// <summary>
    /// 모든 매니저 초기화
    /// </summary>
    private static void Initialize()
    {
        if (_instance != null) return;
        
        Util.FindOrNewComponent(out _instance, DEFAULT_NAME);

        Scene managerScene = SceneManager.CreateScene("Managers");

        SceneManager.MoveGameObjectToScene(_instance.gameObject, managerScene);
        
        _instance._audio.Initialize();
        _instance._item.Initialize();
        _instance._ui.Initialize();
    }
    
}
