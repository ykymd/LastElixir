using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] block;
    [SerializeField]
    private Background background = null;

    public static List<GameObject> blList = new List<GameObject>();
//落ちたブロックの参照保存先
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
            var index = Random.Range(0, block.Length);
            Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
            var obj = Utility.Instantiate(blocks, block[index]);
            obj.GetComponent<Rigidbody2D>().Pause(obj.gameObject);
            obj.transform.position = position;
            obj.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.5f);
            foreach (var collider in obj.GetComponents<BoxCollider2D>())
            {
                collider.enabled = false;
            }
            movingBlock = obj;
        }
        else if (MultiTouch.GetTouch() == TouchInfo.Moved)
        {
            Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
            movingBlock.transform.position = position;
        }
        else if (MultiTouch.GetTouch() == TouchInfo.Ended)
        {
            movingBlock.GetComponent<Rigidbody2D>().Resume(movingBlock.gameObject);
            movingBlock.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
            foreach (var collider in movingBlock.GetComponents<BoxCollider2D>())
            {
                collider.enabled = true;
            }
            blList.Add(movingBlock);//落ちたブロックを順々に保存
            background.TrackObject = movingBlock;
            movingBlock = null;
        }
    }

    public GameObject GetList()
    {
        GameObject obj = blList[0];
        blList.RemoveAt(0);
        return obj;
    }
		
}
