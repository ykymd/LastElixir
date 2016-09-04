using UnityEngine;
using System.Collections;

public class BoomManager : MonoBehaviour
{

    [SerializeField]
    GameObject refObj;
    GameManager gamemanager;
    //リストの中のブロックオブジェクトを参照するため
    [SerializeField]
    CameraManager cameraManager;
    [SerializeField]
    private GameObject SubCamera;
    //サブカメラ
    public GameObject boomPrefab;

    int ListSize;
    GameObject block;

    [SerializeField]
    private GameObject ResultBoard = null;
    float height;

    Transform middlePosition;//中間要素の位置情報


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
        height = gamemanager.GetHighestObject().transform.position.y / 1.25f;
        ListSize = gamemanager.ReturnListSize(); //要素数を保存
        //middlePosition = gamemanager.GetMiddleList();//中間要素の位置情報
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

            if (i != 0 && i == ListSize - 4)//４匹目が爆発するときにズームアウトと減速
            {
                SubCamera.SetActiveRecursively(true);
                cameraManager.CameraZoomOut();
                Destroy(obj, 0.5F);
                yield return new WaitForSeconds(0.1F);
            }
            else if (i < ListSize - 4)//4匹以上が爆発するときは減速
            {
                Destroy(obj, 0.5F);
                yield return new WaitForSeconds(0.1F);
            }
            else//それ以外は普通速度
            {
                Destroy(obj, 1.0F);
                yield return new WaitForSeconds(0.8F);//ここで一回この関数やめる。0.8秒後にここに戻ってきて続きをし始める。
            }
        }

        //ここで全部爆破し終わっている
        yield return new WaitForSeconds(1);
        var resultBoard = Utility.InstantiateGetComponent<ResultBoard>(null, ResultBoard);

        int rank = (int)(height / 5f);
        float next = 5f - (height % 5f);
        resultBoard.SetScore((int)height, rank, next);

    }

}