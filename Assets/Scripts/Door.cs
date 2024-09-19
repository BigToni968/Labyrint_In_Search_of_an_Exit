using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Door : MonoBehaviour
    {
        [field: SerializeField] public int _totalKey { get; private set; }
        [SerializeField] private Vector3 _euler;
        [SerializeField] protected float _durationOpen;

        private Sequence _sequence;
        private IWindowNotify _notify;

        [Inject]
        public void Constructor(IWindowNotify windowNotify)
        {
            _notify = windowNotify;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out IDoorOpener opener))
            {
                if (opener.Kyes() >= _totalKey)
                {
                    _sequence = DOTween.Sequence();
                    _sequence.Append(transform.DOLocalRotate(_euler, _durationOpen));
                    _sequence.Play();
                    StartCoroutine(Win());
                    return;
                }

                _notify.Show(new()
                {
                    Title = "It's still early!",
                    Text = "You must collect all the keys or the doors won't open..",
                    ButtonText = "Ok"
                });
            }
        }

        private IEnumerator Win()
        {
            yield return new WaitForSeconds(2f);
            _notify.Show(new()
            {
                Title = "You winner!",
                Text = "You made it through the labyrinth! Well done! I hope you enjoyed it.",
                ButtonText = "Restart",
                Callback = () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex)
            });
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
