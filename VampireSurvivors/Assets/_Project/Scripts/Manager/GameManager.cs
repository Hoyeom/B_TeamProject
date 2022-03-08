using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyObject;
    public int enemyCount;
    private float delay;
    public GameObject expPrefab;
    private void Start()
    {
        GameObject obj = ObjectPooler.Instance.GenerateGameObject(expPrefab);
        obj.transform.position = new Vector3(-5f, 2, 0f);

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
}