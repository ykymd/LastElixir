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
        seq.Append(transform.DOMoveY(-3.6f + Camera.main.transform.position.y, 1f).SetEase(Ease.InOutBack));
    }

    public void Ended()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(-7f + Camera.main.transform.position.y, 1f).SetEase(Ease.InOutBack).OnComplete(() => Destroy(this.gameObject)));
    }
}
