using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MonsterReference",menuName = "SO/MonsterReference")]
public class EnemyPrefabSO : ScriptableObject
{
    [SerializeField] private LayerMask targetLayer;
    public LayerMask TarGetLayer { get { return targetLayer; } }

    [SerializeField] private GameObject expPrefab;
    public GameObject ExpPrefab { get { return expPrefab; } }

    [SerializeField] private GameObject archerArrow;
    public GameObject ArcherArrow { get { return archerArrow; } }

    [SerializeField] private AudioClip hitSoundClip;
    public AudioClip HitSoundClip { get { return hitSoundClip; } }
}