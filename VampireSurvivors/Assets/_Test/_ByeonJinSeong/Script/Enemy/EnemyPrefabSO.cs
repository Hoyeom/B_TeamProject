using UnityEngine;

[CreateAssetMenu(fileName ="MonsterReference",menuName = "SO/MonsterReference")]
public class EnemyPrefabSO : ScriptableObject
{
    [SerializeField] private LayerMask targetLayer;
    public LayerMask TarGetLayer => targetLayer;

    [SerializeField] private GameObject expPrefab;
    public GameObject ExpPrefab => expPrefab;

    [SerializeField] private GameObject archerArrow;
    public GameObject ArcherArrow => archerArrow;

    [SerializeField] private AudioClip hitSoundClip;
    public AudioClip HitSoundClip => hitSoundClip;
}