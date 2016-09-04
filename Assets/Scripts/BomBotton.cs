using UnityEngine;
using System.Collections;

public class BomBotton : MonoBehaviour
{
    GameObject BomObj;//bomオブジェクトが入る
    public GameObject bomPre;//bomのPrefab
    public bool flag = false;

    void Update()
    {
        if(flag==true){
            MoveBom();
        }
    }

    public void ButtonPush()
    {
        Debug.Log("PutBomPush");
            Vector3 position = MultiTouch.GetTouchWorldPosition(Camera.main);
            //配置する場所と回転角を設定
            Quaternion q = new Quaternion();
            q = Quaternion.identity;
            BomObj = (GameObject)Instantiate(bomPre, position, q);
        flag = true;

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
            Debug.Log("PutBom");
        }
    }
}
