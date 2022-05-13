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
    public GameObject attackPrefab_Skull;

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
}
