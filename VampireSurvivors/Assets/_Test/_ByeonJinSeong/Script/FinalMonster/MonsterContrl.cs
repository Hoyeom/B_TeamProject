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
            Medusa entity = obj.GetComponent<Medusa>();
            entity.Initialize(monsters[i]);
            entitys.Add(entity);
        }
    }

    private void Update()
    {
        // Test용
        //for(int i=0; i < entitys.Count; i++)
        //{
        //    entitys[i].Updated();
        //}
    }
}