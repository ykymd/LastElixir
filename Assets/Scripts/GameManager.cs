using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] block;

    private GameObject blocks;

    void Start()
    {
        blocks = new GameObject("Blocks");
    }

    // Update is called once per frame
    void Update()
    {
        if (MultiTouch.GetTouch() == TouchInfo.Began)
        {
            var index = Random.Range(0, block.Length);
            Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
            var obj = Utility.Instantiate(blocks, block[index]);
            obj.transform.position = position;
        }
    }
}
