using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasParticle : MonoBehaviour
{
    private bool first;
    private float time;

    [SerializeField] private float delaytime;
    [SerializeField] private float lifetime;

    public ParticleSystem[] particleObject = new ParticleSystem[2];

    private void OnEnable()
    {
        time = 0;
        first = true;
        particleObject[1]?.Play();
    }
    private void OnDisable()
    {
        particleObject[0]?.Stop();
        particleObject[1]?.Stop();
    }
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (time > delaytime && first)
        {
            particleObject[0]?.gameObject.SetActive(true);
            particleObject[0]?.Play();
            particleObject[0].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            first = false;
            Destroy(this.gameObject, lifetime);
        }
    }
}
