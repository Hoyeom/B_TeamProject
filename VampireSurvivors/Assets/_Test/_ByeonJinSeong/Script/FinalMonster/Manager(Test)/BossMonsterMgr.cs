using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterMgr : TestSingleTon<BossMonsterMgr>
{
    public Player _player;   // Test 접근지정자 변경 고민

    protected override void Awake()
    {
        base.Awake();
        _player = FindObjectOfType<Player>();
    }


    // 고민 중
    //IEnumerator Move()
    //{
    //    while (true)
    //    {
    //        Vector2 pos = transform.position;
    //        Vector2 playerPos = _player.Player.transform.position;

    //        rigid.MovePosition(rigid.position +
    //                           (Vector2)(playerPos - pos).normalized * curSpeed * Time.deltaTime);
    //        _renderer.flipX = playerPos.x > pos.x;

    //        //PlayerSearch();

    //        yield return new WaitForFixedUpdate();
    //        rigid.velocity = Vector2.zero;
    //    }
    //}

    //void PlayerSearch()
    //{
    //    Collider2D col = Physics2D.OverlapBox(transform.position, size, 0, enemyPrefabSo.TarGetLayer);

    //    if (col != null)
    //    {
    //        curSpeed = 0f;
    //    }
    //    else
    //    {
    //        curSpeed = enemySo.MosterSpeed;
    //        _animator.SetBool(hashAttackAnim, false);

    //        timeset += Time.deltaTime;
    //        timeset %= enemySo.CollTime;
    //    }
    //}
}
