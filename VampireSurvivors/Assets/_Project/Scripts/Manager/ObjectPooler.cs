using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private static ObjectPooler instance = null;
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
    public GameObject GenerateGameObject(GameObject prefab,Transform parent = null)
    {
        int index = 0;

        int hashKey = prefab.GetHashCode();
        
        GameObject idlePrefab = null;

        if (!gameObjects.ContainsKey(hashKey))
        {
            gameObjects.Add(hashKey, new List<GameObject>());
            gameObjects[hashKey].Add(new GameObject(prefab.name));
            gameObjects[hashKey][0].transform.parent = transform;
        }

        for (var i = 1; i < gameObjects[hashKey].Count; i++)
        {
            GameObject obj = gameObjects[hashKey][i];
            if (obj.activeSelf) continue;
            
            index = i;
            idlePrefab = obj;
            break;
        }

        if (idlePrefab == null)
        {
            gameObjects[hashKey].Add(Instantiate(prefab, parent == null ? gameObjects[hashKey][0].transform : parent));
            index = gameObjects[hashKey].Count - 1;
        }
        else
        {
            idlePrefab.transform.parent = parent == null ? gameObjects[hashKey][0].transform : parent;
            idlePrefab.SetActive(true);
        }
        return gameObjects[hashKey][index];
    }
    

    #endregion
    #region DestroyGameObject

    public void AllDestroyGameObject()
    {
        foreach (var key in gameObjects.Keys)
        {
            gameObjects[key][0].transform.parent = transform;
            for (int i = 1; i < gameObjects[key].Count; i++)
            {
                DestroyGameObject(gameObjects[key][i]);
            }
        }
    }

    public void DestroyGameObject(GameObject prefab, float time = 0)
    {
        if (destroyTimer.TryGetValue(prefab, out Coroutine coroutine))
        {
            destroyTimer.Remove(prefab);
            StopCoroutine(coroutine);
        }
        
        if (time > 0)
        {
            destroyTimer.Add(prefab, StartCoroutine(DestroyRoutine(prefab, time)));
        }
        else
        {
            DestroyObject(prefab);
        }

    }

    IEnumerator DestroyRoutine(GameObject prefab,float time)
    {
        yield return new WaitForSeconds(time);
        DestroyObject(prefab);
    }

    private void DestroyObject(GameObject prefab)
    {
        prefab.transform.parent = transform;
        prefab.transform.position = Vector3.zero;
        prefab.transform.eulerAngles = Vector3.zero;
        prefab.SetActive(false);
    }
    
    #endregion

}