using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EventAlert : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        transform.position = transform.position - Vector3.up * 7f;
        Sequence seq = DOTween.Sequence();
        var move = new Vector3(0f, -2f, 1f);
        seq.Append(transform.DOLocalMove(move, 1f).SetEase(Ease.InOutBack));
    }

    public void Ended()
    {
        Sequence seq = DOTween.Sequence();
        var move = new Vector3(0f, -7f, 1f);
        seq.Append(transform.DOLocalMove(move, 1f).SetEase(Ease.InOutBack).OnComplete(() => Destroy(this.gameObject)));
    }
}
