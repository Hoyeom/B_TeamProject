using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour
{
   // private GameObject Missile;
    public  GameObject AttackPrefab;
    private Player _player;
    private Transform target;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        StartCoroutine(ReaperSkill());
    }


    IEnumerator ReaperSkill()
    {
        while (true)
        {
            GameObject Missile = ObjectPooler.Instance.GenerateGameObject(AttackPrefab);
            Vector2 pos = _player.transform.position - AttackPrefab.transform.position;
            float rad = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
            Missile.transform.position = (transform.position * Vector2.one * Random.Range(-1f, 1f));
            Missile.transform.rotation = Quaternion.Euler(0, 0, rad);
            yield return new WaitForSeconds(2f);
        }
    }
}