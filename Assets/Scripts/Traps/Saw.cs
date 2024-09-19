using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Saw : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private Vector2 _distance;
        [SerializeField] private float _diskSinkPause;
        [SerializeField] private float _delayMove;
        [SerializeField] private float _SpeedRotate;
        [SerializeField] private Vector3 _euler;

        private Sequence _seqMove;

        private void Start()
        {
            _seqMove = DOTween.Sequence();
            _seqMove.Append(transform.DOLocalMoveX(_distance.x, _delayMove));
            _seqMove.AppendInterval(_diskSinkPause);
            _seqMove.Append(transform.DOLocalMoveX(_distance.y, _delayMove));
            _seqMove.AppendInterval(_diskSinkPause);
            _seqMove.SetLoops(-1);
            _seqMove.Play();
        }

        private void LateUpdate()
        {
            transform.Rotate(_euler * _SpeedRotate * Time.deltaTime);
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
            _seqMove?.Kill();
        }
    }
}
