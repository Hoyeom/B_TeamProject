using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Select : MonoBehaviour
{
    public GameObject select = null;
    public GameObject option = null;
    public GameObject[] character;
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

    public void PickCharacter1()
    {
        SceneManager.LoadScene("MainScene");
        _player = Instantiate(character[0]);
        _player.transform.position = new Vector3(0, 0, 0);
    }

    public void PickCharacter2()
    {
        SceneManager.LoadScene("MainScene");
        _player = Instantiate(character[1]);
        _player.transform.position = new Vector3(0, 0, 0);
    }

    public void PickCharacter3()
    {
        SceneManager.LoadScene("MainScene");
        _player = Instantiate(character[2]);
        _player.transform.position = new Vector3(0, 0, 0);
    }

    public void PickCharacter4()
    {
        SceneManager.LoadScene("MainScene");
        _player = Instantiate(character[3]);
        _player.transform.position = new Vector3(0, 0, 0);
    }

    public void PickCharacter5()
    {
        SceneManager.LoadScene("MainScene");
        _player = Instantiate(character[4]);
        _player.transform.position = new Vector3(0, 0, 0);
    }
}