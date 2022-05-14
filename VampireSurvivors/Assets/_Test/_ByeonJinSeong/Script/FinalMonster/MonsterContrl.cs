using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum MonsterPro
{
    Noting = 0,
    Skull = 1<<0,
    Medusa = 1<<1,
    Reaper = 1<<2,
    Mantis = 1<<3,
}
public class MonsterContrl : MonoBehaviour
{
    // test 붕떠버린 친구 설계를 잘해야하는 이유
    [Tooltip("대충 설계할 때 발생하는 현상")][SerializeField] private string[] monsters;
    [SerializeField] private GameObject[] monsterPrefab;
    [SerializeField]private MonsterPro monsterpro;

    private List<FMBase> entitys;
    
    private int count;
    private void Awake()
    {
        entitys = new List<FMBase>();

        foreach (MonsterPro flagcheck in Enum.GetValues(typeof(MonsterPro)))
        {
            if (monsterpro.HasFlag(flagcheck))
            {
                if((int)flagcheck == 0) { continue; }
                count = 0;
                CheckShift((int)flagcheck);
                GameObject obj = Instantiate(monsterPrefab[count], transform);
                FMonster entity = obj.GetComponent<FMonster>();
                entity.Initialize(monsterPrefab[count].gameObject.name);
                entitys.Add(entity);
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < entitys.Count; i++)
        {
            entitys[i].Updated();
        }
    }

    private void CheckShift(int num)
    {
        int shift = num>>1;
        if(shift == 0) { return; }
        count++;
        CheckShift(shift);
    }

}