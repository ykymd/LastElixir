using UnityEngine;

namespace Assets.Scripts
{
    public class Background : MonoBehaviour
    {
        [SerializeField]
        private GameManager gameManager = null;

        private void Update()
        {
            GetComponent<SpriteRenderer>().color = gameManager.IsFeverTime ? new Color(255, 0, 255) : new Color(255, 255, 255);
        }
    }
}