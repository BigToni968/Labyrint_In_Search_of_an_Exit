using UnityEngine;

namespace Game
{
    public interface IUnit : IInit, IUpdate { }

    public abstract class Unit : MonoBehaviour, IUnit
    {
        public virtual void OnInit() { }
        public virtual void OnUpdate() { }
    }
}