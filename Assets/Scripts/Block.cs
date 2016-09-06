using UnityEngine;

namespace Assets.Scripts
{
    public class Block : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            if (transform.position.y <= -10f)
            {
                Destroy(gameObject);
            }
        }
    }
}
