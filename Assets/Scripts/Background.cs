using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager = null;

    void Update()
    {
        if (gameManager.IsFeverTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 255);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        }
    }
}