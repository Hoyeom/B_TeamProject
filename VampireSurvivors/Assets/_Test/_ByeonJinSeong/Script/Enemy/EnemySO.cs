using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MonsterConfig", menuName ="SO/MonsterConfig", order = int.MaxValue)]
public class EnemySO : ScriptableObject
{
    [SerializeField] private string monsterName;
    public string MonsyerName { get { return monsterName; } }

    [SerializeField] private float speed;
    public float MosterSpeed { get { return speed; } }

    [SerializeField] private float maxHealth;
    public float MaxHealth { get { return maxHealth; } }

    [SerializeField] private float damage;
    public float Damage { get { return damage; } }

    [SerializeField] private float attackRadius;
    public float AttackRadius { get { return attackRadius; } }

    [SerializeField] private float dropExp;
    public float DropExp { get { return dropExp; } }

    [SerializeField] private float collTime;
    public float CollTime { get { return collTime; } }
}