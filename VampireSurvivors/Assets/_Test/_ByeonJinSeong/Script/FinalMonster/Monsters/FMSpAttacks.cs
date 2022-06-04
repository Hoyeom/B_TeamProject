using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMSpAttacks : MonoBehaviour
{
    float[] probs ;

    [Header("Skull")]
    public GameObject attackPrefab_Skull;

    [Header("Reaper")]
    public GameObject attackPrefab_Reaper;
    public GameObject pingPrefab_Reaper;

    //[Header("Mantis")]

    [Header("Medusa")]
    public GameObject attackPrefab_Medusa;
    public GameObject ciclePrefab_Medusa;

    [Header("Alien")]
    public bool trigger;

    [Header("Zyra")]
    public GameObject spawnPoint;
    public GameObject plantWall;

    #region Skull
    // test 추가 패턴 시 랜덤 버전으로 변경하기
    public void SkullBossSp(Transform StartingPoint)
    {
        GameObject[] skull = new GameObject[4];
        float[] radian = { 0, 72, 144, -144 }; // 공식이 기억안남....
        for (int i = 0; i < skull.Length; i++)
        {
            skull[i] = ObjectPooler.Instance.GenerateGameObject(attackPrefab_Skull);
            skull[i].transform.position = StartingPoint.position;
            skull[i].transform.LookAt(BossMonsterMgr.Inst._player.transform.position);

            skull[i].transform.Rotate(0, 90, radian[i]);
        }
    }
    #endregion

    // 수정 필요함
    #region Reaper
    public void ReaperBossSp()
    {
        GameObject Missile = ObjectPooler.Instance.GenerateGameObject(attackPrefab_Reaper);
        GameObject Ping = ObjectPooler.Instance.GenerateGameObject(pingPrefab_Reaper);

        switch (Random.Range(0, 4))
        {
            case 0: // 위쪽
                Ping.transform.position = Camera.main.ScreenToWorldPoint(
                    new Vector3(Random.Range(0, Screen.width), Screen.height - 0.2f, -Camera.main.transform.position.z));
                break;

            case 1: // 아래쪽
                Ping.transform.position = Camera.main.ScreenToWorldPoint(
                    new Vector3(Random.Range(0, Screen.width), -Screen.height + Screen.height + 0.2f, -Camera.main.transform.position.z));
                break;

            case 2: // 오른쪽
                Ping.transform.position = Camera.main.ScreenToWorldPoint(
                    new Vector3(Screen.width - 0.2f, (Random.Range(0, Screen.height)), -Camera.main.transform.position.z));
                break;

            case 3: // 왼쪽
                Ping.transform.position = Camera.main.ScreenToWorldPoint(
                    new Vector3(-Screen.width + Screen.width + 0.2f, (Random.Range(0, Screen.height)), -Camera.main.transform.position.z));
                break;
        }

        Missile.transform.position = Ping.transform.position;
        Missile.GetComponentInChildren<SpriteRenderer>().enabled = false;
        Vector2 pos = BossMonsterMgr.Inst._player.transform.position - Missile.transform.position;
        float rad = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        Missile.transform.rotation = Quaternion.Euler(0, 0, rad);
        Ping.transform.rotation = Quaternion.Euler(0, 0, rad);

        ObjectPooler.Instance.DestroyGameObject(Ping, 1f);
    }
    #endregion

    public void MantisBossSp(FMonster Mantis)
    {
        Mantis.transform.position = Vector2.Lerp(Mantis.transform.position, BossMonsterMgr.Inst._player.transform.position, Time.deltaTime * 3f);
        if (Vector3.SqrMagnitude(Mantis.transform.position - BossMonsterMgr.Inst._player.transform.position) < 2f)
        {
            Mantis.StateChange(States.Monster_Move);
        }
    }

    public void MedusaBossSp(FMonster Medusa)
    {
        probs = new float[2] { 0.7f, 0.3f };

        int choose = RandomChoose(probs);

        switch (choose)
        {
            case 0:
                GameObject obj = Instantiate(attackPrefab_Medusa);
                obj.transform.position = BossMonsterMgr.Inst._player.transform.position;
                break;

            case 1:
                GameObject _obj = Instantiate(ciclePrefab_Medusa);
                _obj.transform.position = Medusa.gameObject.transform.position;
                break;
                //default:
                //    break;
        }
    }

    public void AlienBossSp(FMonster Alien)
    {
        SpriteRenderer alien= Alien.GetComponent<SpriteRenderer>();
        Color color = alien.color;

        color.a = Alien._Test ? 0.01f : 1f;

        alien.color = color;
    }

    public void ZyraBossSp()
    {
        probs = new float[2]{ 0.7f, 0.3f };

        int choose = RandomChoose(probs);

        switch (choose)
        {
            case 0:
                GameObject obj = ObjectPooler.Instance.GenerateGameObject(spawnPoint);
                obj.transform.position = new Vector2(0, 0);
                obj.transform.Translate(Vector2.right * UnityEngine.Random.Range(-6f, 6f));
                obj.transform.Translate(Vector2.up * UnityEngine.Random.Range(-3f, 3f));
                ObjectPooler.Instance.DestroyGameObject(obj, 5f); // Test
                break;

            case 1:
                for (int i = 0; i < 10; i++)
                {
                    GameObject objwall = ObjectPooler.Instance.GenerateGameObject(plantWall);
                    objwall.transform.position = BossMonsterMgr.Inst._player.transform.position;
                    objwall.transform.Translate(Mathf.Cos(2 * Mathf.PI * i / 10) * 2, Mathf.Sin(2 * Mathf.PI * i / 10) * 2, 0);
                }
                break;
            //default:
            //    break;
        }

    }

    public void OnAudio(AudioClip audio) { if(audio != null) { Managers.Audio.FXPlayerAudioPlay(audio); } }

    private int RandomChoose(float[] probs)
    {
        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i]) { return i; }
            else { randomPoint -= probs[i]; }
        }

        return probs.Length - 1;
    }

}
