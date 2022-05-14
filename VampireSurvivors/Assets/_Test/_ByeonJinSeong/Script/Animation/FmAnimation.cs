using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmAnimation : MonoBehaviour
{
    public AnimEventSo EventChannel;

    private void OnEnable()
    {
        EventChannel.OnEventRaised += Anims;
    }

    private void OnDisable()
    {
        EventChannel.OnEventRaised -= Anims;
    }

    void Anims()
    {
        Debug.Log("애니메이션 실행 중 암튼 애니메이션 실행 중");
    }
}
