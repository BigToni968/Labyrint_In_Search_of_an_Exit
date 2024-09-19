using UnityEngine;
using Zenject;
using TMPro;

namespace Game
{
    public class WindowKey : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        private Door _door;
        private IItemUpCallback _callback;

        [Inject]
        public void Constructor(Door door, Player player)
        {
            _door = door;
            _callback = player;
            _callback.ItemUpCallback += UpdateCounter;
            UpdateCounter(player.Kyes());
        }

        private void UpdateCounter(int amount)
        {
            _counter.SetText($"Keys {amount} / {_door._totalKey}");
        }

        private void OnDestroy()
        {
            _callback.ItemUpCallback -= UpdateCounter;
        }
    }
}