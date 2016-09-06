using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System;
using Assets.Scripts;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] block;
    [SerializeField]
    private GameObject jamBlock;
    /*[SerializeField]
    private Background background = null;*/
    [SerializeField]
    private TrackingCamera trackingcamera = null;//BackGroundの代わりにTrackingCameraにmovingBlock代入
    [SerializeField]
    private Image nextZombieImage = null;
    [SerializeField]
    private Image nextZombieImage2 = null;

    [SerializeField]
    private GameObject SubCamera;
    //サブカメラ

    [SerializeField]
    private GameObject ResultBoard = null;

    [SerializeField]
    private GameObject[] eventAlerts;

    [SerializeField]
    private SoundManager soundManager = null;

    [SerializeField]
    private SoundEffect soundeffect = null;

    private CameraManager _cameMane;
    //カメラチェンジ用のスクリプト

    public static List<GameObject> BlList = new List<GameObject>();
    //落ちたブロックの参照保存先
    private GameObject _blocks;
    private GameObject _movingBlock;
    private bool _zombieMoving = false;
    private List<int> _nextZombie;
    private int _nextZombieNum = -1;

    // Flick
    private Vector3 _startTouchPos = Vector3.zero;
    private Vector3 _endTouchPos = Vector3.zero;
    public bool BomFlag = false;

    // ゾンビを積んだ回数
    private int _piledZombies = 0;
    // 次のイベント発生タイミング
    private int _nextEventTime = -1;
    private int _eventcount = 0;
    private int _eventType = 0;

    // ゲームオーバー判定
    private Rect _judgeArea = new Rect(0, 0, 0, 0);
    private GameObject _judgeTarget = null;
    private bool _isGameOver = false;
    public bool IsFeverTime = false;

    private float _maxHeight = 0f;

    private void Start()
    {
        _blocks = new GameObject("Blocks");

        _nextZombie = GenerateZombieList();
        _nextZombieNum = _nextZombie[0];
        _nextZombie.RemoveAt(0);
        _piledZombies = 0;
        _nextEventTime = GenerateNextEventTime(_piledZombies);
        _eventcount = 0;
        _eventType = 0;
        _isGameOver = false;
        //StartCoroutine(FeverTime());
    }

    private List<int> GenerateZombieList()
    {
        var nextZombieList = new List<int>();
        for (var i = 0; i < block.Length; i++)
        {
            nextZombieList.Add(i);
        }

        //シャッフルする
        return nextZombieList.OrderBy(i => Guid.NewGuid()).ToList();
    }

    private void UpdateNextZombieImage()
    {
        if (_nextZombieNum == -1 || _zombieMoving)
        {
            nextZombieImage.gameObject.SetActive(false);
            //nextZombieImage2.gameObject.SetActive(false);
        }
        else
        {
            nextZombieImage.gameObject.SetActive(true);
            nextZombieImage.sprite = _nextZombieNum != -2 ? block[_nextZombieNum].GetComponent<SpriteRenderer>().sprite : jamBlock.GetComponent<SpriteRenderer>().sprite;
            nextZombieImage2.gameObject.SetActive(true);
            var num = _nextZombie[0];
            nextZombieImage2.sprite = num != -2 ? block[num].GetComponent<SpriteRenderer>().sprite : jamBlock.GetComponent<SpriteRenderer>().sprite;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isGameOver)
            return;

        if (BomFlag == false)
        {
            CheckFlick();
        }
        UpdateNextZombieImage();
        MoveBlock();

        if (Input.GetKeyDown(KeyCode.C))
        {
            Gameover();//C押されたらカメラチェンジ
        }
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (_judgeArea.width == 0)
            return;
        if (_judgeTarget == null) return;
        if (_judgeArea.Contains(_judgeTarget.transform.position)) return;
        var obj = GetHighestObject();
        _maxHeight = obj.transform.position.y;
        FailureAction();
        StartCoroutine(ShowResult());
    }

    private static bool CheckTapMoon()
    {
        var position = MultiTouch.GetTouchPosition();
        var aTapPoint = Camera.main.ScreenToWorldPoint(position);
        var aCollider2D = Physics2D.OverlapPoint(aTapPoint);

        if (!aCollider2D) return false;
        var obj = aCollider2D.transform.gameObject;
        return obj.name == "Moon";
    }

    private void MoveBlock()
    {
        if (!_zombieMoving)
        {
            return;
        }
        switch (MultiTouch.GetTouch())
        {
            case TouchInfo.Moved:
                Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
                _movingBlock.transform.position = position;
                break;
            case TouchInfo.Ended:
                //Time.timeScale = 1.0F;
                if (CheckTapMoon())
                {
                    nextZombieImage.transform.rotation = _movingBlock.transform.rotation;
                    Destroy(_movingBlock);
                    _movingBlock = null;
                    _zombieMoving = false;
                    return;
                }
                _movingBlock.GetComponent<Rigidbody2D>().Resume(_movingBlock.gameObject);
                _movingBlock.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
                foreach (var collider in _movingBlock.GetComponents<BoxCollider2D>())
                {
                    collider.enabled = true;
                }
                BlList.Add(_movingBlock);//落ちたブロックを順々に保存
                if (trackingcamera != null) trackingcamera.TrackObject = _movingBlock;
                _movingBlock = null;
                _zombieMoving = false;
                nextZombieImage.transform.LookAt(nextZombieImage.transform.position + Vector3.forward);
                _nextZombieNum = _nextZombie[0];
                _nextZombie.RemoveAt(0);
                if (_nextZombie.Count() <= 1)
                {
                    var list = GenerateZombieList();
                    _nextZombie.AddRange(list);
                }

                soundeffect.Release.Play();

                // イベント発生の確認
                if (!IsFeverTime) _piledZombies++;
                if (_piledZombies == _nextEventTime)
                {
                    Debug.Log("EVENT!!");
                    soundeffect.EventStart.Play();
                    //eventType = (eventType % 3 == 2) ? 0 : UnityEngine.Random.Range(1, 3);
                    _eventType = (_eventcount % 3 == 2) ? 0 : 1;
                    _eventcount++;
                    _nextEventTime = GenerateNextEventTime(_piledZombies);

                    switch (_eventType)
                    {
                        case 0:
                            // フィーバータイム
                            StartCoroutine(FeverTime());
                            break;
                        case 1:
                            // お邪魔ゾンビ出現
                            StartCoroutine(AppearJamZombie());
                            break;
                    }
                }
                break;
        }
    }

    private IEnumerator FeverTime()
    {
        var alert = Utility.InstantiateGetComponent<EventAlert>(Camera.main.gameObject, eventAlerts[0]);
        Freeze();
        IsFeverTime = true;
        soundManager.StartFeverTime();

        yield return new WaitForSeconds(10f);

        alert.Ended();
        IsFeverTime = false;
        soundManager.FinishFeverTime();
    }

    private IEnumerator AppearJamZombie()
    {
        var alert = Utility.InstantiateGetComponent<EventAlert>(Camera.main.gameObject, eventAlerts[1]);
        _nextZombie.Insert(0, -2);

        yield return new WaitForSeconds(2f);

        alert.Ended();
    }

    private int GenerateNextEventTime(int piledZombie)
    {
        // 10 16
        return piledZombie + UnityEngine.Random.Range(3, 6);
    }

    public void Freeze()
    {
        foreach (var bl in BlList)
        {
            bl.GetComponent<Rigidbody2D>().Pause(bl.gameObject);
        }
    }

    public void GenerateZombie()
    {
        if (_isGameOver)
            return;

        var position = MultiTouch.GetTouchWorldPosition(Camera.main);
        var prefab = _nextZombieNum == -2 ? jamBlock : block[_nextZombieNum];
        var obj = Utility.Instantiate(_blocks, prefab);
        obj.GetComponent<Rigidbody2D>().Pause(obj.gameObject);
        obj.transform.position = position;
        Debug.Log("ZombiePosition" + obj.transform.position);
        obj.transform.rotation = nextZombieImage.transform.rotation;
        obj.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.5f);
        foreach (var boxCollider2D in obj.GetComponents<BoxCollider2D>())
        {
            boxCollider2D.enabled = false;
        }
        _movingBlock = obj;
        _zombieMoving = true;
        soundeffect.Catch.Play();

        obj.GetComponent<GetTopPosition>().CollisionAction = GetTop;
    }

    private void GetTop(GameObject obj)//Actionでひも付けて呼び出されている
    {
        Debug.Log("Cpllision");
        var lastList = GetLastList(); //最後尾のブロック要素をオブジェクトに保存

        if (lastList == null) return;
        var position = lastList.transform.position;//そのブロックの座標を取ってくる
        Debug.Log(position.y);
        //var obj = GetHighestObject();
        //Debug.Log("HIGH:" + obj.transform.position);

        const float range = 1f;
        _judgeArea = new Rect(position.x - range, position.y - range / 2, range * 2, range);
        Debug.Log(_judgeArea);
        _judgeTarget = obj;
        if (IsFeverTime)
            obj.GetComponent<Rigidbody2D>().Pause(lastList.gameObject);
        //obj.GetComponent<GetTopPosition>().enabled = false;
    }

    public GameObject GetHighestObject()
    {
        GameObject highest = null;
        foreach (var obj in BlList)
        {
            if (highest == null)
            {
                highest = obj;
            }
            else
            {
                if (obj.transform.position.y > highest.transform.position.y)
                {
                    highest = obj;
                }
            }
        }

        return highest;
    }

    public GameObject GetLastList()//Listの最後尾の要素を渡す
    {
        if (BlList.Count == 0)
        {
            return null;
        }
        var obj = BlList[BlList.Count - 1];
        return obj;
    }

    public GameObject GetAndRemoveList()
    {
        GameObject obj = BlList[BlList.Count - 1];//Listの最後尾の要素を渡す
        BlList.RemoveAt(BlList.Count - 1);//そして消す、次回呼ばれたときに最後尾から順々に渡すために
        return obj;
    }

    public Transform GetMiddleList()
    {
        var obj = BlList[BlList.Count / 2];//Listの中間の要素を渡す
        Debug.Log("MiddleOriginalPosi" + obj.transform.position.y);
        return obj.transform;//中間要素の位置情報渡す
    }

    private void CheckFlick()
    {
        if (MultiTouch.GetTouch() == TouchInfo.Began)
        {
            _startTouchPos = MultiTouch.GetTouchPosition();
        }
        else if (MultiTouch.GetTouch() == TouchInfo.Ended)
        {
            _endTouchPos = MultiTouch.GetTouchPosition();
            var direction = _endTouchPos - _startTouchPos;

            var aTapPoint = Camera.main.ScreenToWorldPoint(_startTouchPos);
            var aCollider2D = Physics2D.OverlapPoint(aTapPoint);

            if (direction.x < 0)
            {
                Debug.Log("LEFT");
                soundeffect.AngleChange.Play();
                nextZombieImage.transform.Rotate(new Vector3(0, 0, 90f));
            }
            else if (direction.x > 0)
            {
                Debug.Log("RIGHT");
                soundeffect.AngleChange.Play();
                nextZombieImage.transform.Rotate(new Vector3(0, 0, -90f));
            }
        }
    }

    public int ReturnListSize()//Listの要素数を返す
    {
        return BlList.Count;
    }

    public void Gameover()
    {//カメラのチェンジ
        SubCamera.SetActive(true);
        _cameMane = SubCamera.GetComponent<CameraManager>();
        _cameMane.CameraZoomOut();
    }

    private void FailureAction()
    {
        _isGameOver = true;
        var listSize = ReturnListSize(); //要素数を保存
        soundManager.currentBgm.Stop();
        soundeffect.Collapse.Play();

        for (var i = listSize - 1; i >= 0; i--)
        {
            var block = GetAndRemoveList(); //最後尾のブロック要素をオブジェクトに保存
            block.GetComponent<Rigidbody2D>().Resume(block.gameObject);
            block.GetComponent<Rigidbody2D>().velocity = new Vector3(1.0f * UnityEngine.Random.Range(-5.0f, 5.0f), 10.0f * UnityEngine.Random.value, 0);
        }
    }

    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(3);
        soundManager.StartResult();
        var resultBoard = Utility.InstantiateGetComponent<ResultBoard>(null, ResultBoard);
        var height = _maxHeight / 1.25f;
        var rank = (_isGameOver) ? -1 : (int)(height / 5f);
        var next = 5f - (height % 5f);
        resultBoard.SetScore((int)height, rank, next);
    }
}
