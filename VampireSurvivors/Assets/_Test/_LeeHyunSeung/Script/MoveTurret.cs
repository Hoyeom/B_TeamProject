using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTurret : MonoBehaviour
{
    public Transform playerMove;        // 플레이어 위치

    // 20초 마다 이동
    private void Start()
    {
        InvokeRepeating("ImgMove", 0f, 20f);
    }

    // 이미지가 플레이어를 따라 이동
    void ImgMove()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.transform.position = playerMove.position;
    }
}
