using UnityEngine;
using System.Collections;

public class BoomManager : MonoBehaviour
{

    [SerializeField]
    GameObject refObj;
    GameManager gamemanager;
    //リストの中のブロックオブジェクトを参照するため
    public GameObject boomPrefab;

    int ListSize;
    GameObject block;


    // Use this for initialization
    void Start()
    {
        gamemanager = refObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {//スペースキー押したらその一回だけ呼ぶ
            StartBoom();
        }
    }

    public void StartBoom()
    {
        ListSize = gamemanager.ReturnListSize(); //要素数を保存
        StartCoroutine("DelayMethod");

    }

    private IEnumerator DelayMethod(){

        for (int i = ListSize - 1; i >= 0; i--)
        {
            block = gamemanager.GetAndRemoveList(); //最後尾のブロック要素をオブジェクトに保存
            Vector2 position = block.transform.position;//そのブロックの座標を取ってくる
            Destroy(block);//ブロックは消す
            //配置する座標を設定
            Vector3 placePosition = position;
            //配置する回転角を設定
            Quaternion q = new Quaternion();
            q = Quaternion.identity;
            //配置
            var obj = Utility.Instantiate(boomPrefab, placePosition, q);
            Destroy(obj, 1.5F);

            yield return new WaitForSeconds(0.8F);//ここで一回この関数やめる。0.8秒後にここに戻ってきて続きをし始める。

        }

  

    }

}