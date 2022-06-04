using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Flags]
public enum MonsterPro
{
    Noting = 0,
    Skull = 1<<0,
    Medusa = 1<<1,
    Reaper = 1<<2,
    Mantis = 1<<3,
    Alien = 1<<4,
    Zyra = 1<<5
}
public class MonsterContrl : MonoBehaviour
{
    [SerializeField] private string[] monsters;
    [SerializeField] private GameObject[] monsterPrefab;
    [SerializeField] private MonsterPro monsterpro;

    private List<FMBase> entitys;

    public static UnityAction<FMonster> fmonster = delegate { };
    private SpriteRenderer render = null;

    private int count;
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        entitys = new List<FMBase>();

        foreach (MonsterPro flagcheck in Enum.GetValues(typeof(MonsterPro)))
        {
            if (monsterpro.HasFlag(flagcheck))
            {
                if ((int)flagcheck == 0) { continue; }
                count = 0;
                CheckShift((int)flagcheck);
                GameObject obj = Instantiate(monsterPrefab[count], transform);
                FMonster entity = obj.GetComponent<FMonster>();
                entity.Initialize(monsterPrefab[count].gameObject.name);
                entitys.Add(entity);
            }
        }

        fmonster += FmonsterDie;
        if (render != null) { render.color = new Color(1, 1, 1, 0); }
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

    private void FmonsterDie(FMonster entity)
    {
        entitys.Remove(entity);
    }
}