using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandyGenerator : MonoBehaviour {

	[SerializeField]
	GameObject refObj;
	GameManager gamemanager;

	// Use this for initialization
	void Start () {
		gamemanager = refObj.GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){//スペースキー押したら
			GetBlockPosition();
		}
	}

	private void GetBlockPosition()
	{
		GameObject block = gamemanager.GetList(); //先頭の要素をオブジェクトに保存
		Vector2 position = block.transform.position;
		Debug.Log (position.x);
	
	}

}