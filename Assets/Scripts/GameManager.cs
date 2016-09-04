using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	[SerializeField]
	private GameObject[] block;
	[SerializeField]
	private Background background = null;
	[SerializeField]
	private Image nextZombieImage = null;

	public static List<GameObject> blList = new List<GameObject>();
	//落ちたブロックの参照保存先
	private GameObject blocks;
	private GameObject movingBlock;
	private bool zombieMoving = false;
	private int nextZombie = -1;


	void Start()
	{
		blocks = new GameObject("Blocks");
		nextZombie = Random.Range(0, block.Length);
	}

	// Update is called once per frame
	void Update()
	{
		UpdateNextZombieImage();

		MoveBlock();
	}

	private void UpdateNextZombieImage()
	{
		if (nextZombie == -1 || zombieMoving)
		{
			nextZombieImage.gameObject.SetActive(false);
		}
		else
		{
			nextZombieImage.gameObject.SetActive(true);
			nextZombieImage.sprite = block[nextZombie].GetComponent<SpriteRenderer>().sprite;
		}
	}

	public void RotateRight()
	{
		nextZombieImage.transform.Rotate(0, 0, -90f);
	}

	public void RotateLeft()
	{
		nextZombieImage.transform.Rotate(0, 0, 90f);
	}

	private void MoveBlock()
	{
		if (!zombieMoving)
		{
			return;
		}
		else if (MultiTouch.GetTouch() == TouchInfo.Moved)
		{
			Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
			movingBlock.transform.position = position;
		}
		else if (MultiTouch.GetTouch() == TouchInfo.Ended)
		{
			//Time.timeScale = 1.0F;
			movingBlock.GetComponent<Rigidbody2D>().Resume(movingBlock.gameObject);
			movingBlock.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
			foreach (var collider in movingBlock.GetComponents<BoxCollider2D>())
			{
				collider.enabled = true;
			}
			blList.Add(movingBlock);//落ちたブロックを順々に保存
            GameObject obj = blList[blList.Count - 1];
            Debug.Log(obj.name);
			background.TrackObject = movingBlock;
			movingBlock = null;
			zombieMoving = false;
			nextZombie = Random.Range(0, block.Length);
		}
	}

	public void GenerateZombie()
	{
		Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
		var obj = Utility.Instantiate(blocks, block[nextZombie]);
		obj.GetComponent<Rigidbody2D>().Pause(obj.gameObject);
		obj.transform.position = position;
		obj.transform.rotation = nextZombieImage.transform.rotation;
		obj.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.5f);
		foreach (var collider in obj.GetComponents<BoxCollider2D>())
		{
			collider.enabled = false;
		}
		movingBlock = obj;
		zombieMoving = true;
 
        obj.GetComponent<GetTopPosition>().CollisionAction = GetTop;
	}

    void GetTop()
    {
        Debug.Log("Cpllision");
        GameObject block = GetLastList(); //最後尾のブロック要素をオブジェクトに保存
        Vector2 position = block.transform.position;//そのブロックの座標を取ってくる
        Debug.Log(position.y);
    }


	public GameObject GetLastList()//Listの最後尾の要素を渡す
	{
		GameObject obj = blList[blList.Count - 1];
		return obj;
	}

	public GameObject GetAndRemoveList()
	{
		GameObject obj = blList[blList.Count - 1];//Listの最後尾の要素を渡す
		blList.RemoveAt(blList.Count - 1);//そして消す、次回呼ばれたときに最後尾から順々に渡すために
		return obj;
	}
		
}
