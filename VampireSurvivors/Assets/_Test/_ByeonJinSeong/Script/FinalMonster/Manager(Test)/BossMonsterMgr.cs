using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  전혀 쓸모없는 매니저 존재가 메모리 낭비 나중에 바꾸기
/// </summary>
public class BossMonsterMgr : TestSingleTon<BossMonsterMgr>
{
    [HideInInspector]public Player _player;   // Test 접근지정자 변경 고민
    public GameEvent SpAttack;
    protected override void Awake()
    {
        base.Awake();
        _player = FindObjectOfType<Player>();
    }

}
