using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{

    public GameObject plantObject;

    private void OnEnable()
    {
        Invoke("Spawn", 3f);
    }

    private void Spawn()
    {
        //this.gameObject.SetActive(false);
        GameObject plant = ObjectPooler.Instance.GenerateGameObject(plantObject);
        plant.transform.position = transform.position;
        //plant.transform.Translate(Vector2.up/2);
        this.gameObject.SetActive(false);
    }
}
