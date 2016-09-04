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
    [SerializeField]
    private Slider rotateSlider = null;

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

        // 回転角を更新
        if (rotateSlider != null)
        {
            var angle = rotateSlider.value * 360f;
            //var imageAngle = nextZombieImage.transform.rotation;
            //imageAngle.z = angle;
            nextZombieImage.transform.Rotate(0, 0, angle);
        }
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
            movingBlock.GetComponent<Rigidbody2D>().Resume(movingBlock.gameObject);
            movingBlock.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
            foreach (var collider in movingBlock.GetComponents<BoxCollider2D>())
            {
                collider.enabled = true;
            }
            blList.Add(movingBlock);//落ちたブロックを順々に保存
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
        obj.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.5f);
        foreach (var collider in obj.GetComponents<BoxCollider2D>())
        {
            collider.enabled = false;
        }
        movingBlock = obj;
        zombieMoving = true;
    }

    public GameObject GetList()
    {
        GameObject obj = blList[0];
        blList.RemoveAt(0);
        return obj;
    }
		
}
