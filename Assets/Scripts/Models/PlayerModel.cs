using UnityEngine;
using System;

namespace Game
{
    [Serializable]
    public class PlayerModel : ICloneable
    {
        public float Health;
        public float SpeedWalk;
        [Min(1)] public float SpeedRunMultiplier = 1;
        public int AmountKey;
        public CameraSetting CameraSetting;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    [Serializable]
    public struct CameraSetting
    {
        public Vector2 Sensitivity;
        public Vector2 ClampEulers;
    }
}