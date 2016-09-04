using UnityEngine;
using System.Collections;

public class BoomManager : MonoBehaviour {

    [SerializeField]
    GameObject refObj;
    GameManager gamemanager;
    //リストの中のブロックオブジェクトを参照するため

    public GameObject boomPrefab;

    // Use this for initialization
    void Start () {
        gamemanager = refObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {//スペースキー押したら
            StartBoom();
        }
    }

    private void StartBoom()
    {
        GameObject block = gamemanager.GetAndRemoveList(); //最後尾のブロック要素をオブジェクトに保存
        Vector2 position = block.transform.position;//そのブロックの座標を取ってくる
        Destroy(block);//ブロックは消す
        //配置する座標を設定
        Vector3 placePosition = position;
        //配置する回転角を設定
        Quaternion q = new Quaternion();
        q= Quaternion.identity;
        //配置
        var obj = Utility.Instantiate(boomPrefab,placePosition,q);
        Destroy(obj, 1.5F);
    }
}