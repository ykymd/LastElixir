using UnityEngine;
using System.Collections;

public class TrackingCamera : MonoBehaviour
{
    [SerializeField]
    private Background background = null;

    // Update is called once per frame
    void Update()
    {
        if (background == null || background.TrackObject == null)
        {
            var pos = Vector3.zero;
            pos.z = -10f;
            this.transform.position = pos;
            //background.transform.position = Vector3.zero;
            return;
        }

        var position = background.TrackObject.transform.position;
        position.z = this.transform.position.z;
        this.transform.position = position;
    }
}
