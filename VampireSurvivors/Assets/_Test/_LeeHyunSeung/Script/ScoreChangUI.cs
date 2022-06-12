using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreChangUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timeCountText;
    [SerializeField] private TMP_Text stageCountText;
    [SerializeField] private TMP_Text monsterCountText;
    [SerializeField] private TMP_Text levelCountText;

    public void ChangeText()
    {
        timeCountText.text = $"{Managers.Game.Room.playTime:N2}";
        stageCountText.text = $"{Managers.Game.Room.stageIndex}";
        monsterCountText.text = $"{Managers.Game.Room.totalEnemyCount + Managers.Game.Room.killMonsterCount}";
        levelCountText.text = $"{Managers.Game.Player.Level}"; 
    }
}
