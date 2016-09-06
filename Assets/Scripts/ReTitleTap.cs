using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ReTitleTap : MonoBehaviour
    {
        public void ButtonPush()
        {
            SceneManager.LoadScene ("Title");
        }
    }
}
