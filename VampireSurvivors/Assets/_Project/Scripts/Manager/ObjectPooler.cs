using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public Dictionary<int,List<GameObject>> gameObjects = new Dictionary<int, List<GameObject>>();
    
    void Awake()
    {
        Instance = this;
    }
    #region GenerateGameObject
    public GameObject GenerateGameObject(GameObject prefab,Transform parent = null)
    {
        int index = 0;

        int hashKey = prefab.GetHashCode();
        
        GameObject idlePrefab = null;

        if (!gameObjects.ContainsKey(hashKey))
            gameObjects.Add(hashKey, new List<GameObject>());

        for (var i = 0; i < gameObjects[hashKey].Count; i++)
        {
            GameObject obj = gameObjects[hashKey][i];
            if (obj.activeSelf) continue;
            
            index = i;
            idlePrefab = obj;
            break;
        }

        if (idlePrefab == null)
        {
            gameObjects[hashKey].Add(Instantiate(prefab,parent));
            index = gameObjects[hashKey].Count - 1;
        }
        else
        {
            idlePrefab.transform.parent = parent;
            idlePrefab.SetActive(true);
        }
        return gameObjects[hashKey][index];
    }
    
    #endregion
    #region DestroyGameObject
    public void DestroyGameObject(GameObject prefab)
    {
        prefab.transform.parent = transform;
        prefab.SetActive(false);
    }
    #endregion
}