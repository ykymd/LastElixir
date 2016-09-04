using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    [SerializeField]
    GameObject refObj;
    [SerializeField]
    private Background background = null;
    [SerializeField]
    GameManager gamemanager;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    //中間要素のy座標に背景の中心座標を合わせてしまっているので、あとでTrackingCameraと、Backgroundの背景追従やめてシンプルにカメラがゾンビ追うだけにして最後に中間要素のところにカメラの中心y座標奥

    public void CameraZoomOut(){
        Debug.Log("MiddlePosiS"+gamemanager.GetMiddleList().position.y);
        //background.transform.position = new Vector3 (0.0F,gamemanager.GetMiddleList().position.y,-1f);
        //this.transform.position = refObj.transform.position + new Vector3 (0, 0, -10);
        Debug.Log("previoustCamera"+refObj.transform.position);
        Debug.Log("nextCamera"+this.transform.position);
        background.TrackObject = null ;//これでBackGroundスクリプトから怒られなくなる
        refObj.SetActiveRecursively(false);
    }
}
