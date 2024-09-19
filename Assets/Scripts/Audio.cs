using UnityEngine;

namespace Game
{
    public interface IAudio
    {
        public AudioSource Music { get; }
        public AudioSource Sound { get; }
    }

    public class Audio : MonoBehaviour, IAudio
    {
        [field: SerializeField] public AudioSource Music { get; private set; }
        [field: SerializeField] public AudioSource Sound { get; private set; }
    }
}
