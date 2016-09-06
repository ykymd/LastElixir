using UnityEngine;

namespace Assets.Scripts
{
    public class BomBotton : MonoBehaviour
    {
        GameObject _bomObj;
        //bomオブジェクトが入る
        public GameObject BomPre;
        //bomのPrefab
        public bool Flag = false;

        [SerializeField]
        BoomManager boomManager;
        [SerializeField]
        GameManager gameManager;

        private void Update()
        {
            if (Flag == true)
            {
                MoveBom();
            }
        }

        public void ButtonPush()
        {
            Debug.Log("PutBomPush");
            gameManager.BomFlag = true;
            MakeBom();
            Flag = true;//ボムが動いている間のフラグ

        }

        private void MakeBom()
        {
            var position = MultiTouch.GetTouchWorldPosition(Camera.main);
            //配置する場所と回転角を設定
            var q = Quaternion.identity;
            _bomObj = (GameObject)Instantiate(BomPre, position, q);
            //BomObj.transform.position = BomObj.transform.position + new Vector3(3.0F, 0, 0);
        }

        private void MoveBom()
        {
            switch (MultiTouch.GetTouch())
            {
                case TouchInfo.Moved:
                    {
                        Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
                        _bomObj.transform.position = position;
                    }
                    break;
                case TouchInfo.Ended:
                    {
                        Flag = false;
                        Debug.Log("PutBom");
                        Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
                        var collition2D = Physics2D.OverlapPoint(position + new Vector2(-0.8F, 0));

                        if (collition2D)
                        {//ここで爆破判定
                            boomManager.StartBoom();
                            Destroy(_bomObj);
                        }
                        else
                        {
                            Destroy(_bomObj);
                            Debug.Log("BomFaled");
                            gameManager.BomFlag = false;
                        }

                    }
                    break;
            }
        }
    }
}