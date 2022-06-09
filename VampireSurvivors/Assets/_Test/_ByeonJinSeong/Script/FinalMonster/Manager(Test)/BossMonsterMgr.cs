using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterMgr
{

    public GameEvent SpAttack;
    public AnimEventSo anievents;

    public void Initialize()
    {
        SpAttack = Managers.Resource.Load<GameEvent>("SPAttackEvent", true);
        anievents = Managers.Resource.Load<AnimEventSo>("AniEvents", true);
    }

}
