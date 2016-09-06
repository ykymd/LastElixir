using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class RetryTap : MonoBehaviour
    {
        public void ButtonPush()
        {
            SceneManager.LoadScene ("Main");
        }
    }
}
