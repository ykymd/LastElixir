using UnityEngine;

namespace Assets.Scripts
{
    public class MouseDrag : MonoBehaviour
    {
        void Start()
        {
            gameObject.GetComponent<Rigidbody2D>().Pause(gameObject);
        }
    }
}
