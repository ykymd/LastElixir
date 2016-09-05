using UnityEngine;
using System.Collections;

public class TrackingCamera : MonoBehaviour
{
    public GameObject TrackObject;
    //追うターゲットゾンビ、GameManagerで代入される

    // Update is called once per frame
    void Update()
    {
        if (TrackObject == null)
        {//いなかったら何もしない
            return;
        }
        else
        {
            var position = TrackObject.transform.position;//GameManagerからTrackObject代入されてればその位置とる
            position.z = this.transform.position.z;//このカメラの深度（z軸値）代入
            this.transform.position = position;//このカメラの位置をゾンビのところに移動
        }

    }
}
