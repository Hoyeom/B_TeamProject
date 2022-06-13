using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreChangUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timeCountText;
    [SerializeField] private TMP_Text stageCountText;
    [SerializeField] private TMP_Text monsterCountText;
    [SerializeField] private TMP_Text levelCountText;

    [SerializeField] private Image charcterImge;
    [SerializeField] private TMP_Text charcterNameText;
    Item[] itemIcon = null;
    public GameObject slot = null;
    Image[] slotImge = null; 

    public void ChangeImage()
    {
        charcterImge.sprite = Managers.Game.Player.model.GetComponentInChildren<SpriteRenderer>().sprite;
        charcterNameText.text = $"{Managers.Game.Player.model.GetComponentInChildren<SpriteRenderer>().gameObject.name}";
        HasItem();
    }

    private void HasItem()
    {
        itemIcon = Managers.Game.Player.GetComponentsInChildren<Item>();
        slotImge = slot.GetComponentsInChildren<Image>();
        Debug.Log(slotImge.Length);
        for(int i =0; i< slotImge.Length; i++)
        {
            if(i< itemIcon.Length)
            {
                Debug.Log("매칭");
                slotImge[i].sprite = itemIcon[i].spriteImg;

            }
            else 
            {
                Debug.Log("비어있다");
                slotImge[i].color = new Color(1, 1, 1, 0);
            }
        }
        
    }

    public void ChangeText()
    {
        timeCountText.text = $"{Managers.Game.Room.playTime:N2}";
        stageCountText.text = $"{Managers.Game.Room.stageIndex}";
        monsterCountText.text = $"{Managers.Game.Room.totalEnemyCount + Managers.Game.Room.killMonsterCount}";
        levelCountText.text = $"{Managers.Game.Player.Level}"; 
    }
}
