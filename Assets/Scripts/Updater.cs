using UnityEngine;
using Zenject;

namespace Game
{
    public class Updater : MonoBehaviour
    {
        [SerializeField] private Unit[] _units;

        [Inject] private IPause _pause;

        private void Start()
        {
            for (int i = 0; i < _units.Length; i++)
            {
                _units[i].OnInit();
            }
        }

        private void Update()
        {

            if (_pause.Status)
            {
                return;
            }

            for (int i = 0; i < _units.Length; i++)
            {
                _units[i].OnUpdate();
            }
        }
    }
}
