using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    RandumRoom nextStage;

    private void OnEnable()
    {
        nextStage = GameObject.Find("GameManager(Test)").GetComponent<RandumRoom>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log(collision.gameObject.name);
            nextStage.NextStage();
        }
    }
}
