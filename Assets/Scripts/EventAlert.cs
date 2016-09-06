using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    public class EventAlert : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            transform.position = transform.position - Vector3.up * 7f;
            var seq = DOTween.Sequence();
            var move = new Vector3(0f, -2f, 1f);
            seq.Append(transform.DOLocalMove(move, 1f).SetEase(Ease.InOutBack));
        }

        public void Ended()
        {
            var seq = DOTween.Sequence();
            var move = new Vector3(0f, -7f, 1f);
            seq.Append(transform.DOLocalMove(move, 1f).SetEase(Ease.InOutBack).OnComplete(() => Destroy(gameObject)));
        }
    }
}
