using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Distributed : MonoBehaviour
{

    [SerializeField]
    GameObject refObj;

    GameManager gamemanager;
    //リストの中のブロックオブジェクトを参照するため

    // Use this for initialization
    void Start()
    {
        gamemanager = refObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {//Dキー押したら
            ZonDis();
        }
    }

    private void ZonDis()
    {
        int ListSize = gamemanager.ReturnListSize(); //要素数を保存
        GameObject block;

        for (int i = ListSize - 1; i >= 0; i--)
        {
            block = gamemanager.GetAndRemoveList(); //最後尾のブロック要素をオブジェクトに保存
            block.GetComponent<Rigidbody2D>().velocity = new Vector3(1.0f * Random.Range(-5.0f,5.0f), 10.0f * Random.value,0);
        }
      
    }

}