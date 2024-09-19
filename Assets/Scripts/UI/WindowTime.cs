using System.Collections;
using UnityEngine;
using Zenject;
using System;
using TMPro;

namespace Game
{
    public class WindowTime : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _watch;
        private float _time;
        private WaitForSeconds _wait;
        private WaitWhile _waitPause;
        private IPause _pause;
        private Coroutine _coroutine;

        [Inject]
        public void Constructor(IPause pause)
        {
            _pause = pause;
        }

        private void Start()
        {
            _wait = new(1f);
            _waitPause = new(PauseStatus);
            _coroutine = StartCoroutine(TimeUp());
        }

        private IEnumerator TimeUp()
        {
            while (true)
            {
                yield return _wait;
                yield return _waitPause;
                _time++;
                TimeSpan time = TimeSpan.FromSeconds(_time);
                _watch.SetText(string.
                    Format("Watch H {0:D2} : M {1:D2}: S {2:D2}",
                    time.Hours,
                    time.Minutes,
                    time.Seconds));
            }
        }

        private bool PauseStatus()
        {
            return _pause.Status;
        }

        private void OnDestroy()
        {
            StopCoroutine(_coroutine);
        }
    }
}
