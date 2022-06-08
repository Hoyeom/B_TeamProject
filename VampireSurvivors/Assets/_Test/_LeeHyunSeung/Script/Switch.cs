using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject[] gate = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            for(int i= 0; i < gate.Length; i++)
            {
                gate[i].SetActive(true);
            }
            Managers.Game.Room.MonsterCount();
            gameObject.SetActive(false);
        }
    }
}
