using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    [SerializeField]
    GameObject refObj;
    [SerializeField]
    private TrackingCamera trackingcamera = null;
    [SerializeField]
    GameManager gamemanager;

    public Vector3 middleYposi;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    //中間要素のy座標に背景の中心座標を合わせてしまっているので、あとでTrackingCameraと、Backgroundの背景追従やめてシンプルにカメラがゾンビ追うだけにして最後に中間要素のところにカメラの中心y座標奥

    public void CameraZoomOut(){
        //Debug.Log("MiddlePosiS"+gamemanager.GetMiddleList().position.y);
        //this.transform.position = refObj.transform.position + new Vector3 (0, 0, -10);
        //Debug.Log("previoustCamera"+refObj.transform.position);
        //Debug.Log("nextCamera"+this.transform.position);
        //trackingcamera.TrackObject = null ;//これでBackGroundスクリプトから怒られなくなる
        Debug.Log("ChangeCamera!!andMiddle"+middleYposi.y);
        this.transform.position = new Vector3 (0.0F,middleYposi.y,-10f);;
        refObj.SetActiveRecursively(false);
    }
}
