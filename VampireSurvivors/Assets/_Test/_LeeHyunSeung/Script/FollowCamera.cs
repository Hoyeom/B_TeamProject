using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowCamera : MonoBehaviour
{
    private Transform player = null;
    CinemachineVirtualCamera followCamera = null;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        followCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        followCamera.Follow = player;
    }
}
