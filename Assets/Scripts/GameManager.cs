using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] block;

	public static List<GameObject> blList = new List<GameObject> ();//落ちたブロックの参照保存先
    private GameObject blocks;
    private GameObject movingBlock;


    void Start()
    {
        blocks = new GameObject("Blocks");
    }

    // Update is called once per frame
    void Update()
    {
        MoveBlock();
    }

    private void MoveBlock()
    {
        if (MultiTouch.GetTouch() == TouchInfo.Began)
        {
            Debug.Log("Touch Start");
            var index = Random.Range(0, block.Length);
            Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
            var obj = Utility.Instantiate(blocks, block[index]);
            obj.GetComponent<Rigidbody2D>().Pause(obj.gameObject);
            obj.transform.position = position;
            movingBlock = obj;
        }
        else if (MultiTouch.GetTouch() == TouchInfo.Moved)
        {
            Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
            movingBlock.transform.position = position;
        }
        else if (MultiTouch.GetTouch() == TouchInfo.Ended)
        {
            Debug.Log("Touch End");
            movingBlock.GetComponent<Rigidbody2D>().Resume(movingBlock.gameObject);
			blList.Add(movingBlock);//落ちたブロックを順々に保存
            movingBlock = null;
        }
    }

	public GameObject GetList(){
		GameObject obj = blList[0];
		blList.RemoveAt(0);
		return obj;
	}
		
}
