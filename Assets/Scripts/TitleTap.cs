using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class TitleTap : MonoBehaviour
    {
        private void Update()
        {
            if (MultiTouch.GetTouch() == TouchInfo.Began)//ワンクリックまたはタッチした場合
            {
                SceneManager.LoadScene("Main");
            }
        }

    }
}