using UnityEngine;
using System.Collections;


public class BomBotton : MonoBehaviour
{
    GameObject BomObj;
    //bomオブジェクトが入る
    public GameObject bomPre;
    //bomのPrefab
    public bool flag = false;

    [SerializeField]
    BoomManager boomManager;
    [SerializeField]
    GameManager gameManager;

    void Update()
    {
        if (flag == true)
        {
            MoveBom();
        }
    }

    public void ButtonPush()
    {
        Debug.Log("PutBomPush");
        gameManager.BomFlag = true;
        MakeBom();
        flag = true;//ボムが動いている間のフラグ

    }

    private void MakeBom()
    {
        Vector3 position = MultiTouch.GetTouchWorldPosition(Camera.main);
        //配置する場所と回転角を設定
        Quaternion q = new Quaternion();
        q = Quaternion.identity;
        BomObj = (GameObject)Instantiate(bomPre, position, q);
        //BomObj.transform.position = BomObj.transform.position + new Vector3(3.0F, 0, 0);
    }

    private void MoveBom()
    {
        if (MultiTouch.GetTouch() == TouchInfo.Moved)
        {
            Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
            BomObj.transform.position = position;
        }
        else if (MultiTouch.GetTouch() == TouchInfo.Ended)
        {
            flag = false;
            Debug.Log("PutBom");
            Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
            Collider2D collition2d = Physics2D.OverlapPoint(position + new Vector2(-0.8F, 0)); 

            if (collition2d)
            {//ここで爆破判定
                boomManager.StartBoom();
                Destroy(BomObj);
            }
            else
            {
                Destroy(BomObj);
                Debug.Log("BomFaled");
                gameManager.BomFlag = false;
            }

        }
    }
}