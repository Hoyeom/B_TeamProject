using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTurret : MonoBehaviour
{
    public Transform playerMove;

    private void Start()
    {
        InvokeRepeating("ImgMove", 0f, 20f);
    }

    void ImgMove()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.transform.position = playerMove.position;
    }
}
