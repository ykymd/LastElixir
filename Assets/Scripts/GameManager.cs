using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System;

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
    private List<int> nextZombie;
    private int nextZombieNum = -1;


    void Start()
    {
        blocks = new GameObject("Blocks");

        nextZombie = GenerateZombieList();
        nextZombieNum = nextZombie[0];
        nextZombie.RemoveAt(0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNextZombieImage();

        MoveBlock();
    }

    private List<int> GenerateZombieList()
    {
        var nextZombieList = new List<int>();
        for (int i = 0; i < block.Length; i++)
        {
            nextZombieList.Add(i);
        }

        //シャッフルする
        return nextZombieList.OrderBy(i => Guid.NewGuid()).ToList();
    }

    private void UpdateNextZombieImage()
    {
        if (nextZombieNum == -1 || zombieMoving)
        {
            nextZombieImage.gameObject.SetActive(false);
        }
        else
        {
            nextZombieImage.gameObject.SetActive(true);
            nextZombieImage.sprite = block[nextZombieNum].GetComponent<SpriteRenderer>().sprite;
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
            background.TrackObject = movingBlock;
            movingBlock = null;
            zombieMoving = false;
            nextZombieNum = nextZombie[0];
            nextZombie.RemoveAt(0);
            if (nextZombie.Count() <= 0)
                nextZombie = GenerateZombieList();
        }
    }

    public void GenerateZombie()
    {
        Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
        var obj = Utility.Instantiate(blocks, block[nextZombieNum]);
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
    }

    public void Freeze()
    {
        foreach (var block in blList)
        {
            block.GetComponent<Rigidbody2D>().Pause(block.gameObject);
        }
    }

    public GameObject GetList()
    {
        GameObject obj = blList[0];
        blList.RemoveAt(0);
        return obj;
    }
		
}
