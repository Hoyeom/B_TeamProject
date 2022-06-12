using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOver = null;
    public GameObject gameClear = null;

    Animator anim_Over = null;
    Animator anim_Clear = null;

    ScoreChangUI scorChang = null;

    //[SerializeField] private TMP_Text timeCountText;
    //[SerializeField] private TMP_Text stageCountText;
    //[SerializeField] private TMP_Text monsterCountText;
    //[SerializeField] private TMP_Text levelCountText;

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
        anim_Over.gameObject.SetActive(true);
        anim_Over.SetTrigger("GameOver");
        scorChang = gameOver.GetComponent<ScoreChangUI>();
        scorChang.ChangeText();
    }

    public void OnGameClear()
    {
        anim_Clear.gameObject.SetActive(true);
        anim_Clear.SetTrigger("GameOver");
        scorChang = gameClear.GetComponent<ScoreChangUI>();
        scorChang.ChangeText();
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
        Debug.Log("Back");
        ObjectPooler.Instance.AllDestroyGameObject();
        Managers.Item.ItemClear();
        SceneManager.LoadScene("MainMenu");
    }

    //private void ChangeText()
    //{
    //    timeCountText = transform.Find("Time Number").GetComponent<TMP_Text>();
    //    stageCountText = transform.Find("Stage Number").GetComponent<TMP_Text>();
    //    monsterCountText = transform.Find("Monster Number").GetComponent<TMP_Text>();
    //    levelCountText = transform.Find("Level Number").GetComponent<TMP_Text>();
    //    timeCountText.text = $"{(float)System.DateTime.Now.TimeOfDay.TotalSeconds }";
    //    stageCountText.text = $"{Managers.Game.Room.stageIndex}";
    //    monsterCountText.text = $"{Managers.Game.Room.totalEnemyCount+Managers.Game.Room.killMonsterCount}";
    //    //levelCountText.text = $"{}"; 
    //} 
}
