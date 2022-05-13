using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum MonsterPro
{
    //Noting = 0,
    Skull = 1<<0,
    Medusa = 1<<1,
    Reaper = 1<<2
}
public class MonsterContrl : MonoBehaviour
{
    // test 붕떠버린 친구 설계를 잘해야하는 이유
    [Tooltip("대충 설계할 때 발생하는 현상")][SerializeField] private string[] monsters;
    [SerializeField] private GameObject[] monsterPrefab;

    private List<FMBase> entitys;
    public MonsterPro monsterpro;

    private void Awake()
    {
        entitys = new List<FMBase>();
        
        // Test
        foreach(MonsterPro flagcheck in Enum.GetValues(typeof(MonsterPro)))
        {
            if (monsterpro.HasFlag(flagcheck))
            {
                //Debug.Log((int)flagcheck / 2);
                GameObject obj = Instantiate(monsterPrefab[(int)flagcheck / 2], transform);
                FMonster entity = obj.GetComponent<FMonster>();
                entity.Initialize(monsterPrefab[(int)flagcheck / 2].gameObject.name);
                entitys.Add(entity);
            }
        }
    }

    // Test용 전부 소환 추 후 변경
    /// <summary>
    /// 작업 목록
    /// 1. 소환될 보스 선택 정하기
    /// 1-1. 번호지정
    /// 1-2. 미리 소환?
    /// 1-3. Flag
    /// </summary>
    private void FixedUpdate()
    {
    
        for (int i = 0; i < entitys.Count; i++)
        {
            entitys[i].Updated();
        }
    }

}