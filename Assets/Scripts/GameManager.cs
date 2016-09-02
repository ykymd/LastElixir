using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject block = null;

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
            Vector2 position = MultiTouch.GetTouchWorldPosition(Camera.main);
            var obj = Utility.Instantiate(blocks, block);
            obj.transform.position = position;
        }
    }
}
