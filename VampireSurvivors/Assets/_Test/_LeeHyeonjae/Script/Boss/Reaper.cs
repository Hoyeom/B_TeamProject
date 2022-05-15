using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour
{
   // private GameObject Missile;
    public  GameObject AttackPrefab;
    public GameObject PingPrefab;
    private Player _player;
    private Transform a;
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
            GameObject missile = ObjectPooler.Instance.GenerateGameObject(AttackPrefab);
            GameObject ping = ObjectPooler.Instance.GenerateGameObject(PingPrefab);
            switch(Random.Range(0,4))
            {
                case 0: // 위쪽
                    ping.transform.position = Camera.main.ScreenToWorldPoint(
                        new Vector3(Random.Range(0, Screen.width), Screen.height-0.2f, -Camera.main.transform.position.z));
                    break;

                case 1: // 아래쪽
                    ping.transform.position = Camera.main.ScreenToWorldPoint(
                        new Vector3(Random.Range(0, Screen.width), -Screen.height + Screen.height + 0.2f, -Camera.main.transform.position.z));
                    break;

                case 2: // 오른쪽
                    ping.transform.position = Camera.main.ScreenToWorldPoint(
                        new Vector3(Screen.width-0.2f, (Random.Range(0 , Screen.height)), -Camera.main.transform.position.z));
                    break;

                case 3: // 왼쪽
                    ping.transform.position = Camera.main.ScreenToWorldPoint(
                        new Vector3(-Screen.width + Screen.width+0.2f, (Random.Range(0,Screen.height)), -Camera.main.transform.position.z));
                    break;
            }
            missile.transform.position = ping.transform.position;
            missile.GetComponentInChildren<SpriteRenderer>().enabled = false;
            ObjectPooler.Instance.DestroyGameObject(ping,1f);
            Vector2 pos = _player.transform.position - missile.transform.position;
            float rad = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
            missile.transform.rotation = Quaternion.Euler(0, 0, rad);
            ping.transform.rotation = Quaternion.Euler(0, 0, rad);
            yield return new WaitForSeconds(2f);
        }
    }
}