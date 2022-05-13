using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterMgr : TestSingleTon<BossMonsterMgr>
{
    public Player _player;   // Test 접근지정자 변경 고민
    public GameEvent SpAttack;
    protected override void Awake()
    {
        base.Awake();
        _player = FindObjectOfType<Player>();
    }

}
