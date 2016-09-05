using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    [SerializeField]
    private AudioSource clicked = null;

    public void GoToTitleScene()
    {
        clicked.Play();
        SceneManager.LoadScene("Title");
    }

    public void Retry()
    {
        clicked.Play();
        SceneManager.LoadScene("Main");
    }
}
