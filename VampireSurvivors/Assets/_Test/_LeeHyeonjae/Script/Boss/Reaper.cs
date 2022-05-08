using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour
{
   // private GameObject Missile;
    public  GameObject AttackPrefab;
    public GameObject PingPrefab;
    private Player _player;
    private Transform target;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        StartCoroutine(ReaperSkill());
    }


    public IEnumerator ReaperSkill()
    {
        while (true)
        {
            GameObject Missile = ObjectPooler.Instance.GenerateGameObject(AttackPrefab);
            GameObject Ping = ObjectPooler.Instance.GenerateGameObject(PingPrefab);
            switch(Random.Range(0,4))
            {
                case 0: // 위쪽
                    Ping.transform.position = Camera.main.ScreenToWorldPoint(
                        new Vector3(Random.Range(0, Screen.width), Screen.height-0.2f, -Camera.main.transform.position.z));
                    Missile.transform.position = Ping.transform.position;
                    Missile.GetComponent<SpriteRenderer>().enabled = false;
                    Debug.Log(Missile.transform.position);
                    Debug.DrawRay(_player.transform.position, Missile.transform.position - _player.transform.position, Color.green, 1);
                    break;

                case 1: // 아래쪽
                    Ping.transform.position = Camera.main.ScreenToWorldPoint(
                        new Vector3(Random.Range(0, Screen.width), -Screen.height + Screen.height + 0.2f, -Camera.main.transform.position.z));
                    Missile.transform.position = Ping.transform.position;
                    Missile.GetComponent<SpriteRenderer>().enabled = false;
                    Debug.Log(Missile.transform.position);
                    Debug.DrawRay(_player.transform.position, Missile.transform.position - _player.transform.position, Color.green, 1);
                    break;

                case 2: // 오른쪽
                    Ping.transform.position = Camera.main.ScreenToWorldPoint(
                        new Vector3(Screen.width-0.2f, (Random.Range(0 , Screen.height)), -Camera.main.transform.position.z));
                    Missile.transform.position = Ping.transform.position;
                    Missile.GetComponent<SpriteRenderer>().enabled = false;
                    Debug.Log(Missile.transform.position);
                    Debug.DrawRay(_player.transform.position, Missile.transform.position - _player.transform.position, Color.green, 1);
                    break;

                case 3: // 왼쪽
                    Ping.transform.position = Camera.main.ScreenToWorldPoint(
                        new Vector3(-Screen.width + Screen.width+0.2f, (Random.Range(0,Screen.height)), -Camera.main.transform.position.z));
                    Missile.transform.position = Ping.transform.position;
                    Missile.GetComponent<SpriteRenderer>().enabled = false;
                    Debug.Log(Missile.transform.position);
                    Debug.DrawRay(_player.transform.position, Missile.transform.position - _player.transform.position, Color.green, 1);
                    break;
            }
            Vector2 pos = _player.transform.position - Missile.transform.position;
            float rad = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
            Missile.transform.rotation = Quaternion.Euler(0, 0, rad);
            yield return new WaitForSeconds(2f);
        }
    }
}