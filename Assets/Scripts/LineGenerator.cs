using UnityEngine;
using System.Collections;

public class LineGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject linePrefab = null;
    [SerializeField]
    private GameObject scientist = null;

    // 1mは1.25f
    // Use this for initialization
    void Start()
    {
        var parent = new GameObject("Lines");

        for (int i = 1; i <= 10; i++)
        {
            var line = Utility.Instantiate(parent, linePrefab);
            var position = line.transform.position;
            var sp = scientist.GetComponent<SpriteRenderer>().sprite;
            position.y = -1.8f + 1.25f * 5 * i;
            line.transform.position = position;
        }
    }
}
