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
        DontDestroyOnLoad(gameObject);
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
        

        SceneManager.sceneLoaded += GenerateObject1;
    }
    
    public void PickCharacter2()
    {
        SceneManager.LoadScene("MainScene");
        
        
        SceneManager.sceneLoaded += GenerateObject2;
    }

    public void PickCharacter3()
    {
        SceneManager.LoadScene("MainScene");
        SceneManager.sceneLoaded += GenerateObject3;
    }

    public void PickCharacter4()
    {
        SceneManager.LoadScene("MainScene");
        SceneManager.sceneLoaded += GenerateObject4;
    }

    public void PickCharacter5()
    {
        SceneManager.LoadScene("MainScene");
        SceneManager.sceneLoaded += GenerateObject5;
    }
    
    private void GenerateObject1(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= GenerateObject1;
        _player = Instantiate(character[0]);
        Destroy(gameObject);
    }
    
    private void GenerateObject2(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= GenerateObject2;
        _player = Instantiate(character[1]);
        Destroy(gameObject);
    }
    
    private void GenerateObject3(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= GenerateObject3;
        _player = Instantiate(character[2]);
        Destroy(gameObject);
    }
    
    private void GenerateObject4(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= GenerateObject4;
        _player = Instantiate(character[3]);
        Destroy(gameObject);
    }
    
    private void GenerateObject5(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= GenerateObject5;
        _player = Instantiate(character[4]);
        Destroy(gameObject);
    }
}