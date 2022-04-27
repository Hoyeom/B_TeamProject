using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnStart()
    {
        this.gameObject.SetActive(false);
    }

    public void UIRestart()
    {
        anim.SetTrigger("GameOver");
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
