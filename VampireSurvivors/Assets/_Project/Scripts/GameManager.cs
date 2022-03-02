using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyObject;
    private float delay;
    
    private void Start()
    {
        StartCoroutine(SpawnEnemy(2, 2));
    }

    IEnumerator SpawnEnemy(float delay, float time)
    {
        yield return new WaitForSeconds(delay);


        while (true)
        {
            GameObject obj = ObjectPooler.Instance.GenerateGameObject(enemyObject);
            obj.transform.position = transform.position;

            yield return new WaitForSeconds(time);
        }
    }
    
    
}
