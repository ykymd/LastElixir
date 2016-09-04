using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandyGenerator : MonoBehaviour
{

	[SerializeField]
	GameObject refObj;

	[SerializeField]
	private GameObject candy;
//画像

	GameManager gamemanager;
//リストの中のブロックオブジェクトを参照するため
	private GameObject candies;
//candyオブジェクトの親

	// Use this for initialization
	void Start()
	{
		gamemanager = refObj.GetComponent<GameManager>();
		candies = new GameObject("Candies");//candyオブジェクトの親を作る、一括で管理しやすくなる
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{//スペースキー押したら
			GetBlockPosition();
		}
	}

	private void GetBlockPosition()
	{
		GameObject block = gamemanager.GetList(); //先頭のブロック要素をオブジェクトに保存
		Vector2 position = block.transform.position;//そのブロックの座標を取ってくる
		var obj = Utility.Instantiate(candies, candy);//親の指定と画像指定
		//obj.GetComponent<Rigidbody2D>().Pause(obj.gameObject);
		Destroy(block);//ブロックは消す
		obj.transform.position = position;//ゾンビがいた位置に表示
		//Debug.Log (position.x);
	}

}