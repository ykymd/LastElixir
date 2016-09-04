using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    [SerializeField]
    GameObject refObj;
    [SerializeField]
    private Background background = null;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void CameraZoomOut(){
        this.transform.position = refObj.transform.position + new Vector3 (0, 0, -10);;
        Debug.Log("previoustCamera"+refObj.transform.position);
        Debug.Log("nextCamera"+this.transform.position);
        background.TrackObject = null ;//これでBackGroundスクリプトから怒られなくなる
        refObj.SetActiveRecursively(false);
    }
}
