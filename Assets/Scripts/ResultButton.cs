using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    public void GoToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main");
    }
}
