using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private static ObjectPooler instance = null;
    /// <summary>
    /// 풀러가 없다면 생성한다
    /// </summary>
    public static ObjectPooler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (ObjectPooler) new GameObject("ObjectPooler").AddComponent(typeof(ObjectPooler));
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    
    private Dictionary<int,List<GameObject>> gameObjects = new Dictionary<int, List<GameObject>>();
    private Dictionary<GameObject, Coroutine> destroyTimer = new Dictionary<GameObject, Coroutine>();
    
    #region GenerateGameObject
    
    /// <summary>
    /// 해당 오브젝트를 새로 생성하거나 풀에 있는 오브젝트를 가져온다
    /// </summary>
    /// <param name="prefab">원본 프리펩</param>
    /// <param name="parent">부모 객체</param>
    /// <returns></returns>
    public GameObject GenerateGameObject(GameObject prefab,Transform parent = null)
    {
        return Instantiate(prefab, parent);
        //int index = 0;

        //int hashKey = prefab.GetHashCode();
        
        //GameObject idlePrefab = null;

        //if (!gameObjects.ContainsKey(hashKey))
        //{
        //    gameObjects.Add(hashKey, new List<GameObject>());
        //    gameObjects[hashKey].Add(new GameObject(prefab.name));
        //    gameObjects[hashKey][0].transform.parent = transform;
        //}

        //for (var i = 1; i < gameObjects[hashKey].Count; i++)
        //{
        //    GameObject obj = gameObjects[hashKey][i];
        //    if (obj.activeSelf) continue;
            
        //    index = i;
        //    idlePrefab = obj;
        //    break;
        //}

        //if (idlePrefab == null)
        //{
        //    gameObjects[hashKey].Add(Instantiate(prefab, parent == null ? gameObjects[hashKey][0].transform : parent));
        //    index = gameObjects[hashKey].Count - 1;
        //}
        //else
        //{
        //    idlePrefab.transform.parent = parent == null ? gameObjects[hashKey][0].transform : parent;
        //    idlePrefab.SetActive(true);
        //}
        //return gameObjects[hashKey][index];
    }
    

    #endregion
    #region DestroyGameObject

    /// <summary>
    /// 풀링된 오브젝트를 모두 풀로 되돌린다
    /// </summary>
    public void AllDestroyGameObject()
    {
        foreach (var objs in gameObjects.Values)
        {
            foreach (var obj in objs)
            {
                Destroy(obj);
            }
            objs.Clear();
        }
        gameObjects.Clear();
        destroyTimer.Clear();
        StopAllCoroutines();
    }

    /// <summary>
    /// 풀러 되돌린다
    /// </summary>
    /// <param name="prefab">발사체 또는 풀링된 오브젝트</param>
    /// <param name="time">N초 후 풀로 반환</param>
    public void DestroyGameObject(GameObject prefab, float time = 0)
    {
        Destroy(prefab, time);
        //if (destroyTimer.TryGetValue(prefab, out Coroutine coroutine))
        //{
        //    destroyTimer.Remove(prefab);
        //    StopCoroutine(coroutine);
        //}
        
        //if (time > 0)
        //{
        //    destroyTimer.Add(prefab, StartCoroutine(DestroyRoutine(prefab, time)));
        //}
        //else
        //{
        //    DestroyObject(prefab);
        //}

    }
    
    IEnumerator DestroyRoutine(GameObject prefab,float time)
    {
        yield return new WaitForSeconds(time);
        DestroyObject(prefab);
    }

    /// <summary>
    /// 풀로 반환
    /// </summary>
    /// <param name="prefab"></param>
    private void DestroyObject(GameObject prefab)
    {
        prefab.transform.parent = transform;
        prefab.transform.position = Vector3.zero;
        prefab.transform.eulerAngles = Vector3.zero;
        prefab.SetActive(false);
    }
    
    #endregion

}