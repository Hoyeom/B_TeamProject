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
        followCamera = GetComponent<CinemachineVirtualCamera>();
        StartCoroutine(FindPlayer());
    }

    IEnumerator FindPlayer()
    {
        GameObject obj= null;

        while(player != null)
        {
            obj = GameObject.Find("Player");
            yield return null;
        }

        player = obj.transform;
        followCamera.Follow = player;
    }

}
