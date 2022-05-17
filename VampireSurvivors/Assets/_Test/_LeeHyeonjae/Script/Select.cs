using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Select : MonoBehaviour
{
    public GameObject select = null;
    public GameObject option = null;
    public GameObject[] Char;
    private GameObject _player;

    //public AudioMixer masterMixer = null;
    //public Slider audioSlider = null;

    public void OnStart()
    {
        select.SetActive(true);
    }

    public void OnOption()
    {
        option.SetActive(true);
    }

    public void OnBack()
    {
        if (select.activeSelf == true)
        {
            select.SetActive(false);
        }

        if (option.activeSelf == true)
        {
            option.SetActive(false);
        }
    }

    //public void AudioController()
    //{
    //    float sound = audioSlider.value;

    //    if(sound == -40.0f)
    //    {
    //        masterMixer.SetFloat("", -80);
    //    }
    //    else
    //    {
    //        masterMixer.SetFloat("", sound);
    //    }
    //}

    public void OnStage()
    {
        SceneManager.LoadScene("Test");
       _player = ObjectPooler.Instance.GenerateGameObject(Char[0]);
        _player.transform.position = new Vector3(0, 0, 0);
    }
}