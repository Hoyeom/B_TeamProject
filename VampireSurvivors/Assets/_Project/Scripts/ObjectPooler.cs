using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<GameObject> gameObjects;
    
    void Awake()
    {
        Instance = this;
        gameObjects = new List<GameObject>();
    }

    public GameObject GenerateGameObject(GameObject prefab)
    {
        int index = 0;
        
        GameObject idlePrefab = null;

        for (var i = 0; i < gameObjects.Count; i++)
        {
            GameObject obj = gameObjects[i];
            if (obj.activeSelf) continue;
            index = i;
            idlePrefab = obj;
            break;
        }

        if (idlePrefab == null)
        {
            gameObjects.Add(Instantiate(prefab));
            index = gameObjects.Count - 1;
        }
        else
        {
            idlePrefab.transform.parent = null;
            idlePrefab.SetActive(true);
        }

        return gameObjects[index];
    }
    
    public void DestroyGameObject(GameObject prefab)
    {
        prefab.transform.parent = transform;
        prefab.SetActive(false);
    }
    
    public void DestroyGameObject(GameObject prefab,float time)
    {
        StartCoroutine(DestroyObject(prefab, time));
    }


    IEnumerator DestroyObject(GameObject prefab, float time)
    {
        yield return new WaitForSeconds(time);
        prefab.transform.parent = transform;
        prefab.SetActive(false);
        yield return null;
    }
}

