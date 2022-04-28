using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSpec", menuName = "SO/MonsterSpec")]
public class FMSpecSO : ScriptableObject
{
    // 중복이 많음 나중에 수정
    [SerializeField] private float monsterSpeed;
    public float MonsterSpeed => monsterSpeed;

    [SerializeField] private float attackSpeed;
    public float AttackSpeed => attackSpeed;

    [SerializeField] private float maxHealth;
    public float MaxHealth => maxHealth;

    [SerializeField] private float damage;
    public float Damage => damage;

    [SerializeField] private float def;
    public float Def => def;

    [SerializeField] private float dropExp;
    public float DropExp => dropExp;

    [SerializeField] private float collTime;
    public float CollTime => collTime;

    [SerializeField] private float attackRange;
    public float AttackRange => attackRange;

    [SerializeField] private LayerMask targetLayer;
    public LayerMask TarGetLayer => targetLayer;
}