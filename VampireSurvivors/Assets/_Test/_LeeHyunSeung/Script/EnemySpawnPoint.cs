using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject enemy = null;
    public int enemyCount = 0;
    public float interval = 0.2f;

    // 오브젝트 활성화시 생성
    private void OnEnable()
    {
        StartCoroutine(Spowner(1));       
    }

    // 한번만 생성후 종료
    private void Update()
    {
        StopCoroutine(Spowner(0));
    }

    IEnumerator Spowner(float delay )
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject obj = ObjectPooler.Instance.GenerateGameObject(enemy);
            obj.transform.position = transform.position;

            // 몬스터간의 간격
            yield return new WaitForSeconds(interval);
        }
    }
    //private void EnemySpown()
    //{
    //    for(int i = 0;i < enemyCount; i++ )
    //    {
    //        time += Time.deltaTime;
    //        if(time >delay)
    //        { 
    //            time = 0;
    //            GameObject obj = ObjectPooler.Instance.GenerateGameObject(enemy);
    //            obj.transform.position = transform.position;
    //        }
    //    }
        
    //}
}
