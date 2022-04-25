using UnityEngine;

[CreateAssetMenu(fileName ="MonsterConfig", menuName ="SO/MonsterConfig", order = int.MinValue)]
public class EnemySO : ScriptableObject
{
    [SerializeField] private string monsterName;
    public string MonsyerName => monsterName;

    [SerializeField] private float speed;
    public float MosterSpeed => speed;

    [SerializeField] private float maxHealth;
    public float MaxHealth => maxHealth;

    [SerializeField] private float damage;
    public float Damage => damage;

    [SerializeField] private float attackRadius;
    public float AttackRadius => attackRadius;

    [SerializeField] private float dropExp;
    public float DropExp => dropExp;

    [SerializeField] private float collTime;
    public float CollTime => collTime;
}