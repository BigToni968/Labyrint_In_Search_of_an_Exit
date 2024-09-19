using UnityEngine;
using System;

namespace Game
{
    public interface IPauseCallback
    {
        public delegate void PauseDelegate(bool status);
        public event PauseDelegate PauseCallback;
    }

    public interface IPause : IPauseCallback
    {
        public bool Status { get; }

        public void SetStatus(bool newStatus);
    }

    [Serializable]
    public class Pause : IPause
    {
        [field: SerializeField] public bool Status { get; private set; }

        public event IPauseCallback.PauseDelegate PauseCallback;

        public void SetStatus(bool newStatus)
        {
            Status = newStatus;
            PauseCallback?.Invoke(Status);
        }
    }
}