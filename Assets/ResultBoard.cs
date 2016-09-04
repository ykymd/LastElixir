using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultBoard : MonoBehaviour
{
    [SerializeField]
    private Text Height = null;
    [SerializeField]
    private Text Power = null;
    [SerializeField]
    private Text NextRank = null;

    public void SetScore(int height, int rank, int next)
    {
        Height.text = string.Format("{0}m", height);
        Power.text = string.Format("{0}級", rank);
        NextRank.text = string.Format("{0}m", next);
    }
}
