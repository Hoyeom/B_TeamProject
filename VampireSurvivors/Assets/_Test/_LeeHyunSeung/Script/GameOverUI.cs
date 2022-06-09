using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOver = null;
    public GameObject gameClear = null;

    Animator anim_Over = null;
    Animator anim_Clear = null;
    private void Awake()
    {
        anim_Over = gameOver.GetComponent<Animator>();
        anim_Clear = gameClear.GetComponent<Animator>();
    }

    private void Start()
    {
        Managers.Game.Player.OnPlayerDead += OnGameOver;
    }

    public void OnGameOver()
    {
        anim_Over.SetTrigger("GameOver");
    }

    public void OnGameClear()
    {
        anim_Clear.SetTrigger("GameOver");
    }

    public void OnQuit()
    {
        GameObject score_Clear = gameClear.transform.Find("Score").gameObject;
        score_Clear.SetActive(true);
        GameObject score_Over = gameOver.transform.Find("Score").gameObject;
        score_Over.SetActive(true);
    }

    public void OnBack()
    {
        SceneManager.LoadScene("MainUI");
    }
}
