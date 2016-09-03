using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] block;
    [SerializeField]
    private Background background = null;

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
            if (background != null)
            {
                background.TrackObject = movingBlock;
            }
            movingBlock = null;
        }
    }
}
