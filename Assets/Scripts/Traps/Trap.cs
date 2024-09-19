using UnityEngine;

namespace Game
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private Vector3 _euler;

        private void LateUpdate()
        {
            transform.Rotate(_euler * _rotateSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.Hit(_damage);
            }
        }
    }
}