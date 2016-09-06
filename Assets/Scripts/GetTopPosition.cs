using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class GetTopPosition : MonoBehaviour
    {
        private bool _isTouched = true;

        public Action<GameObject> CollisionAction;

        //オブジェクトが衝突したとき
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_isTouched || collision.gameObject.tag != "Zombie" || CollisionAction == null) return;
            CollisionAction(gameObject);
            _isTouched = false;
        }
    }
}
