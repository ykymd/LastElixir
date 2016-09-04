using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class ReTitleTap : MonoBehaviour
{
    public void ButtonPush()
    {
        SceneManager.LoadScene ("Title");
    }
}
