using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Data/Units/Player")]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public PlayerModel Model { get; private set; }
    }
}
