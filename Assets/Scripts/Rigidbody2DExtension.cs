//  Rigidbody2DExtension.cs
//  http://kan-kikuchi.hatenablog.com/entry/Pause_Resume
//
//  Created by kan kikuchi on 2015.11.26.

using UnityEngine;

namespace Assets.Scripts
{ //一時停止時の速度を保管するクラス
    public class Velocity2DTmp : MonoBehaviour{
        //一時停止時の速度

        public float AngularVelocity { get; private set; }

        public Vector2 Velocity { get; private set; }

        /// <summary>
        /// Rigidbody2Dを入力して速度を設定する
        /// </summary>
        public void Set(Rigidbody2D rigidbody2D){
            AngularVelocity = rigidbody2D.angularVelocity;
            Velocity        = rigidbody2D.velocity;
        }

    }

    /// <summary>
    /// Rigidbody2D 型の拡張メソッドを管理するクラス
    /// </summary>
    public static class Rigidbody2DExtension {

        /// <summary>
        /// 一時停止
        /// </summary>
        public static void Pause(this Rigidbody2D rigidbody2D, GameObject gameObject){
            gameObject.AddComponent<Velocity2DTmp> ().Set(rigidbody2D);
            rigidbody2D.isKinematic = true;
        }

        /// <summary>
        /// 再開
        /// </summary>
        public static void Resume(this Rigidbody2D rigidbody2D, GameObject gameObject){
            if(gameObject.GetComponent<Velocity2DTmp> () == null){
                return;
            }

            rigidbody2D.velocity        = gameObject.GetComponent<Velocity2DTmp>().Velocity;
            rigidbody2D.angularVelocity = gameObject.GetComponent<Velocity2DTmp>().AngularVelocity;
            rigidbody2D.isKinematic     = false;

            Object.Destroy (gameObject.GetComponent<Velocity2DTmp> ());
        }

    }
}