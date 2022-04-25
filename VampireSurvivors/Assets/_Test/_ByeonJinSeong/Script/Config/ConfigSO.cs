using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "SO/Config", order = int.MaxValue)]
public class ConfigSO : ScriptableObject
{
    [SerializeField] private Player _player;
    public Player Player => _player;
}