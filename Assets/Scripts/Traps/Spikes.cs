using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private Vector2 _height;
        [SerializeField] private float _spearSinkPause;
        [SerializeField] private float _delayUp, _delayDown;

        private Sequence _sequence;

        private void Start()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOLocalMoveY(transform.localPosition.y + _height.x, _delayUp));
            _sequence.AppendInterval(_spearSinkPause);
            _sequence.Append(transform.DOLocalMoveY(transform.localPosition.y + _height.y, _delayDown));
            _sequence.SetLoops(-1);
            _sequence.Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.Hit(_damage);
            }
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
