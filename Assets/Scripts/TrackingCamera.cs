﻿using UnityEngine;

namespace Assets.Scripts
{
    public class TrackingCamera : MonoBehaviour
    {
        public GameObject TrackObject;
        //追うターゲットゾンビ、GameManagerで代入される

        // Update is called once per frame
        private void Update()
        {
            if (TrackObject == null)
            {//いなかったら何もしない
                return;
            }
            var position = TrackObject.transform.position;//GameManagerからTrackObject代入されてればその位置とる
            position.z = transform.position.z;//このカメラの深度（z軸値）代入
            transform.position = position;//このカメラの位置をゾンビのところに移動
        }
    }
}
