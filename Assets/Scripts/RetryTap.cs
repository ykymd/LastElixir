using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class RetryTap : MonoBehaviour
{
    public void ButtonPush()
    {
        SceneManager.LoadScene ("Main");
    }
}
