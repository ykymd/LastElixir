using UnityEngine;
using System.Collections;

public class TimeLimit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*オブジェクトが衝突したとき指定されたコルーチンを呼び出す
    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Cpllision");
        StartCoroutine("DelayMethod");

    }
        
    private IEnumerator DelayMethod(){
        
        yield return new WaitForSeconds(3.0F);//この下のものを３秒後に実行
        Debug.Log("Delay call");
        Time.timeScale = 0.0F;
    }*/

}
