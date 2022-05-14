using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMSpAttacks : MonoBehaviour
{
    /// <summary>
    /// Test
    /// 1. 날먹
    /// 2. 시간이 지나면 설정법 까먹을 가능성 높음 ㅋㅋㅋ
    /// </summary>

    [Header("Skull")]
    public GameObject attackPrefab_Skull;

    [Header("Reaper")]
    public GameObject attackPrefab_Reaper;
    public GameObject pingPrefab_Reaper;

    //[Header("Mantis")]
  

    #region Skull
    // test 추가 패턴 시 랜덤 버전으로 변경하기
    public void SkullBossSp(Transform StartingPoint)
    {
        GameObject[] skull = new GameObject[4];
        float[] radian = { 0, 72, 144, -144 }; // 공식이 기억안남....
        for(int i = 0; i < skull.Length; i++)
        {
            skull[i] = ObjectPooler.Instance.GenerateGameObject(attackPrefab_Skull);
            skull[i].transform.position = StartingPoint.position;
            skull[i].transform.LookAt(BossMonsterMgr.Inst._player.transform.position);

            skull[i].transform.Rotate(0, 90, radian[i]);
        }
    }
    #endregion

    // 수정 필요함
    #region Reaper
    public void ReaperBossSp()
    {
        GameObject Missile = ObjectPooler.Instance.GenerateGameObject(attackPrefab_Reaper);
        GameObject Ping = ObjectPooler.Instance.GenerateGameObject(pingPrefab_Reaper);

        switch (Random.Range(0, 4))
        {
            case 0: // 위쪽
                Ping.transform.position = Camera.main.ScreenToWorldPoint(
                    new Vector3(Random.Range(0, Screen.width), Screen.height - 0.2f, -Camera.main.transform.position.z));
                WhatMathod(Missile, Ping);
                break;

            case 1: // 아래쪽
                Ping.transform.position = Camera.main.ScreenToWorldPoint(
                    new Vector3(Random.Range(0, Screen.width), -Screen.height + Screen.height + 0.2f, -Camera.main.transform.position.z));
                WhatMathod(Missile, Ping);
                break;

            case 2: // 오른쪽
                Ping.transform.position = Camera.main.ScreenToWorldPoint(
                    new Vector3(Screen.width - 0.2f, (Random.Range(0, Screen.height)), -Camera.main.transform.position.z));
                WhatMathod(Missile, Ping);
                break;

            case 3: // 왼쪽
                Ping.transform.position = Camera.main.ScreenToWorldPoint(
                    new Vector3(-Screen.width + Screen.width + 0.2f, (Random.Range(0, Screen.height)), -Camera.main.transform.position.z));
                WhatMathod(Missile, Ping);
                break;
        }

        Vector2 pos = BossMonsterMgr.Inst._player.transform.position - Missile.transform.position;
        float rad = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        Missile.transform.rotation = Quaternion.Euler(0, 0, rad);
    }

    // 현재님 이친구 이름좀 정해주세요
    private void WhatMathod(GameObject Missile, GameObject Ping)
    {
        Missile.transform.position = Ping.transform.position;
        Missile.GetComponent<SpriteRenderer>().enabled = false;
        Debug.DrawRay(BossMonsterMgr.Inst._player.transform.position, Missile.transform.position - BossMonsterMgr.Inst._player.transform.position, Color.green, 1);
    }
    #endregion

    // 일단 대기
    public void MantisBossSp(GameObject Mantis)
    {
        Debug.Log("돌진");
    }
}
