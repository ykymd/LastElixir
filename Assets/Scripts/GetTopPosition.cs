using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GetTopPosition : MonoBehaviour
{
    private bool isTouched = true;

    public Action<GameObject> CollisionAction;

    // Use this for initialization
    void Start()
    {
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    //オブジェクトが衝突したとき
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTouched && collision.gameObject.tag == "Zombie" && CollisionAction != null)
        {
            CollisionAction(this.gameObject);
            isTouched = false;
        }
    }
}
