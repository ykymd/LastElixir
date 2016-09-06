using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class BoomManager : MonoBehaviour
    {

        [SerializeField]
        GameObject refObj;
        GameManager _gamemanager;
        //リストの中のブロックオブジェクトを参照するため
        [SerializeField]
        CameraManager cameraManager;
        [SerializeField]
        private GameObject SubCamera;
        //サブカメラ
        public GameObject boomPrefab;
        [SerializeField]
        private AudioSource bombSE = null;
        [SerializeField]
        private SoundManager soundManager = null;

        private int _listSize;
        private GameObject _block;

        [SerializeField]
        private GameObject ResultBoard = null;

        private float _height;

        Transform _middlePosition;//中間要素の位置情報


        // Use this for initialization
        private void Start()
        {
            _gamemanager = refObj.GetComponent<GameManager>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {//スペースキー押したらその一回だけ呼ぶ
                StartBoom();
            }
        }

        public void StartBoom()
        {
            bombSE.Play();
            Debug.Log("Gamemanagerから読んだMiddleYPosi" + _gamemanager.GetMiddleList().position.y);
            cameraManager.MiddleYposi = new Vector3(0, _gamemanager.GetMiddleList().position.y, 0);//中間要素のy座標をCameraManagerに渡しておく。先にしておかないと爆破destroyでそのゾンビ消えちゃうから
            _height = _gamemanager.GetHighestObject().transform.position.y / 1.25f;
            _listSize = _gamemanager.ReturnListSize(); //要素数を保存
            //middlePosition = gamemanager.GetMiddleList();//中間要素の位置情報
            StartCoroutine(DelayMethod());
        }

        private IEnumerator DelayMethod()
        {
            for (var i = _listSize - 1; i >= 0; i--)
            {

                _block = _gamemanager.GetAndRemoveList(); //最後尾のブロック要素をオブジェクトに保存
                Vector2 position = _block.transform.position;//そのブロックの座標を取ってくる
                Destroy(_block);//ブロックは消す
                //配置する座標を設定
                Vector3 placePosition = position;
                //配置する回転角を設定
                var q = Quaternion.identity;
                //配置
                var obj = Instantiate(boomPrefab, placePosition, q);
                bombSE.Play();

                if (i != 0 && i == _listSize - 4)//４匹目が爆発するときにズームアウトと減速
                {
                    SubCamera.SetActive(true);
                    cameraManager.CameraZoomOut();
                    Destroy(obj, 0.5F);
                    yield return new WaitForSeconds(0.1F);
                }
                else if (i < _listSize - 4)//4匹以上が爆発するときは減速
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

            var rank = (int)(_height / 5f);
            var next = 5f - (_height % 5f);
            resultBoard.SetScore((int)_height, rank, next);
            soundManager.StartResult();
        }

    }
}