using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y <= -10f)
        {
            Destroy(this.gameObject);
        }
    }
}
