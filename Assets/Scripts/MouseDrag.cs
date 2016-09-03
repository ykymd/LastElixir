using UnityEngine;
using System.Collections;

public class MouseDrag : MonoBehaviour
{
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody2D>().Pause(gameObject);
    }

    /*
    void OnMouseDrag()
    {
        Debug.Log("OnMouseDrag");

        Vector3 objectPointInScreen
        = Camera.main.WorldToScreenPoint(this.transform.position);

        Vector3 mousePointInScreen
        = new Vector3(Input.mousePosition.x,
            Input.mousePosition.y,
            objectPointInScreen.z);

        Vector3 mousePointInWorld = Camera.main.ScreenToWorldPoint(mousePointInScreen);
        mousePointInWorld.z = this.transform.position.z;
        this.transform.position = mousePointInWorld;
    }
    */
}
