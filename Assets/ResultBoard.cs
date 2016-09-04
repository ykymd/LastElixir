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

    public void SetScore(float height, int rank, float next)
    {
        Height.text = string.Format("{0}m", ToRoundDown(height, 2));
        Power.text = string.Format("{0}級", GetRank(rank));
        NextRank.text = string.Format("{0}m", ToRoundDown(next, 2));
    }

    private string GetRank(int rank)
    {
        switch (rank)
        {
            case 0:
                return "飼犬逃亡";
            case 1:
                return "両親心配";
            case 2:
                return "近所迷惑";
            case 3:
                return "故郷壊滅";
            case 4:
                return "国家消滅";
            case 5:
                return "大陸沈没";
            case 6:
                return "人類滅亡";
            case 7:
                return "地球消滅";
            case 8:
                return "銀河崩壊";
        }

        if (rank > 9)
            return "神様困惑";

        return "エラー";
    }

    public static double ToRoundDown(float dValue, int iDigits) {
        float dCoef = (float)System.Math.Pow(10, iDigits);

        return dValue > 0 ? System.Math.Floor  (dValue * dCoef) / dCoef:
            System.Math.Ceiling(dValue * dCoef) / dCoef;
    }
}
