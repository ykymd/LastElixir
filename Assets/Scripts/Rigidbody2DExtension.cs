//  Rigidbody2DExtension.cs
//  http://kan-kikuchi.hatenablog.com/entry/Pause_Resume
//
//  Created by kan kikuchi on 2015.11.26.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//一時停止時の速度を保管するクラス
public class Velocity2DTmp : MonoBehaviour{
    //一時停止時の速度
    private float   _angularVelocity;
    private Vector2 _velocity;

    public float AngularVelocity{
        get{return _angularVelocity;}
    }
    public Vector2 Velocity{
        get{return _velocity;}
    }

    /// <summary>
    /// Rigidbody2Dを入力して速度を設定する
    /// </summary>
    public void Set(Rigidbody2D rigidbody2D){
        _angularVelocity = rigidbody2D.angularVelocity;
        _velocity        = rigidbody2D.velocity;
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

        GameObject.Destroy (gameObject.GetComponent<Velocity2DTmp> ());
    }

}  