using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Key : MonoBehaviour
    {
        [SerializeField] private Vector2 _height;
        [SerializeField] private float _delay;

        private Sequence _sequence;

        private void Start()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOLocalMoveY(transform.localPosition.y + _height.x, _delay));
            _sequence.Append(transform.DOLocalMoveY(transform.localPosition.y + _height.y, _delay));
            _sequence.SetLoops(-1, LoopType.Yoyo);
            _sequence.Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IItemUp up))
            {
                up.Take();
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}