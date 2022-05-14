using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ
public class GasParticle : MonoBehaviour
{
    private bool first;
    private float time;

    [SerializeField] private float delaytime;
    [SerializeField] private float lifetime;

    public ParticleSystem[] particleObject = new ParticleSystem[2];

    void Start()
    {
    }
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
    void Update()
    {
        time += Time.deltaTime;
        if (time > delaytime && first)
        {
            particleObject[0]?.Play();
            first = false;
            Destroy(this.gameObject, lifetime);
        }
    }
}
