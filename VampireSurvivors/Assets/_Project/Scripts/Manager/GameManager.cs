using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance {
        get
        {
            instance ??= GameObject.FindObjectOfType<GameManager>();
            return instance;
        }
    }
    private static RoomManager _room;
    private Player _player;
    public static RoomManager Room
    {
        get
        {
            if(_room == null)
                _room = Instance.GetComponent<RoomManager>();
            return _room;
        }
    }
    public Player Player
    {
        get
        {
            if(_player == null)
                _player = GameObject.FindObjectOfType<Player>();
            return _player;
        }
    }
    private static AudioManager _audio;
    public AudioManager Audio
    {
        get
        {
            if(_audio==null)
                Instance.GetComponent<AudioManager>();
            return _audio;
        }
    }

}


#if false

    

    public GameObject enemyObject;
    public int enemyCount;
    private float delay;
    public GameObject expPrefab;

    private void Start()
    {
        if (expPrefab != null)
        {
            GameObject obj = ObjectPooler.Instance.GenerateGameObject(expPrefab);
            obj.transform.position = new Vector3(-5f, 2, 0f); //test
        }

        StartCoroutine(SpawnEnemy(2, 2)); //TEST
    }

    IEnumerator SpawnEnemy(float delay, float time) // TEST
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject obj = ObjectPooler.Instance.GenerateGameObject(enemyObject);
                obj.transform.position = transform.position;
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(time);
        }
    }

#endif
