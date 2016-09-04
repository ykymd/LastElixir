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

    // Flick
    private Vector3 startTouchPos = Vector3.zero;
    private Vector3 endTouchPos = Vector3.zero;

    // ゾンビを積んだ回数
    private int piledZombies = 0;
    // 次のイベント発生タイミング
    private int nextEventTime = -1;
    private int eventcount = 0;
    private int eventType = 0;

    void Start()
    {
        blocks = new GameObject("Blocks");

        nextZombie = GenerateZombieList();
        nextZombieNum = nextZombie[0];
        nextZombie.RemoveAt(0);
        piledZombies = 0;
        nextEventTime = piledZombies + UnityEngine.Random.Range(10, 16);
        eventcount = 0;
        eventType = 0;
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

    // Update is called once per frame
    void Update()
    {
        CheckFlick();
        UpdateNextZombieImage();

        MoveBlock();
    }

    private bool CheckTapMoon()
    {
        var position = MultiTouch.GetTouchPosition();
        var aTapPoint = Camera.main.ScreenToWorldPoint(position);
        var aCollider2d = Physics2D.OverlapPoint(aTapPoint);

        if (aCollider2d)
        {
            GameObject obj = aCollider2d.transform.gameObject;
            return obj.name == "Moon";
        }

        return false;
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
            if (CheckTapMoon())
            {
                nextZombieImage.transform.rotation = movingBlock.transform.rotation;
                Destroy(movingBlock);
                movingBlock = null;
                zombieMoving = false;
                return;
            }
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
            nextZombieImage.transform.LookAt(nextZombieImage.transform.position + Vector3.forward);
            nextZombieNum = nextZombie[0];
            nextZombie.RemoveAt(0);
            if (nextZombie.Count() <= 0)
                nextZombie = GenerateZombieList();

            // イベント発生の確認
            piledZombies++;
            if (piledZombies == nextEventTime)
            {
                Debug.Log("EVENT!!");
                eventcount++;
                eventType = (eventType % 3 == 2) ? 0 : UnityEngine.Random.Range(1, 3);
                nextEventTime = piledZombies + UnityEngine.Random.Range(10, 16);
            }
        }
    }

    public void Freeze()
    {
        foreach (var block in blList)
        {
            block.GetComponent<Rigidbody2D>().Pause(block.gameObject);
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
 
        obj.GetComponent<GetTopPosition>().CollisionAction = GetTop;
    }

    void GetTop()//Actionでひも付けて呼び出されている
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

    private void CheckFlick()
    {
        if (MultiTouch.GetTouch() == TouchInfo.Began)
        {
            startTouchPos = MultiTouch.GetTouchPosition();
        }
        else if (MultiTouch.GetTouch() == TouchInfo.Ended)
        {
            endTouchPos = MultiTouch.GetTouchPosition();
            var direction = endTouchPos - startTouchPos;

            var aTapPoint = Camera.main.ScreenToWorldPoint(startTouchPos);
            var aCollider2d = Physics2D.OverlapPoint(aTapPoint);

            if (direction.x < 0)
            {
                Debug.Log("LEFT");
                nextZombieImage.transform.Rotate(new Vector3(0, 0, 90f));
            }
            else if (direction.x > 0)
            {
                Debug.Log("RIGHT");
                nextZombieImage.transform.Rotate(new Vector3(0, 0, -90f));
            }
        }
    }

    public int ReturnListSize()//Listの要素数を返す
    {
        return blList.Count;
    }
}
