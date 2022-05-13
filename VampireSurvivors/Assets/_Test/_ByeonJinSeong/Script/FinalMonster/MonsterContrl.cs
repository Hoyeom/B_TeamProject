using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterContrl : MonoBehaviour
{
    [SerializeField] private string[] monsters;
    [SerializeField] private GameObject monsterPrefab;

    private List<FMBase> entitys;

    private void Awake()
    {
        entitys = new List<FMBase>();

        // Test
        for(int i = 0; i < monsters.Length; i++)
        {
            GameObject obj = Instantiate(monsterPrefab, transform);
            FMonster entity = obj.GetComponent<FMonster>();
            entity.Initialize(monsters[i]);
            entitys.Add(entity);
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