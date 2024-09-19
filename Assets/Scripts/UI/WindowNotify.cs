using UnityEngine.UI;
using UnityEngine;
using Zenject;
using System;
using TMPro;

namespace Game
{
    public interface IWindowNotify
    {
        public void Show(Message message);
    }

    public class WindowNotify : MonoBehaviour, IWindowNotify
    {
        [SerializeField] private Canvas _self;
        [SerializeField] private UI_Notify _notify;

        public delegate void DelegateWindowNotify();

        private IPause _pause;
        private Message _default;
        private DelegateWindowNotify _callback;

        [Inject]
        public void Constructor(IPause pause)
        {
            _pause = pause;
            _default = new()
            {
                Title = _notify.Title.text,
                Text = _notify.Text.text,
                ButtonText = _notify.ButtonText.text
            };
        }

        private void FillNotify(Message message)
        {
            _notify.Title.SetText(message.Title);
            _notify.Text.SetText(message.Text);
            _notify.ButtonText.SetText(message.ButtonText);
            _callback = message.Callback;
        }

        private void ClearNotify()
        {
            _notify.Title.SetText(_default.Title);
            _notify.Text.SetText(_default.Text);
            _notify.ButtonText.SetText(_default.ButtonText);
            _callback?.Invoke();
            _callback = null;
        }

        public void Show(Message message)
        {
            FillNotify(message);
            _pause.SetStatus(_self.enabled = true);
            _pause.PauseCallback += PauseCallback;
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void Hide()
        {
            ClearNotify();
            _pause.PauseCallback -= PauseCallback;
            _pause.SetStatus(_self.enabled = false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void PauseCallback(bool status)
        {
            if (!status && _self.enabled)
            {
                _pause.PauseCallback -= PauseCallback;
                _pause.SetStatus(true);
                Cursor.lockState = CursorLockMode.Confined;
                _pause.PauseCallback += PauseCallback;
            }
        }
    }

    [Serializable]
    public class UI_Notify
    {
        [field: SerializeField] public TextMeshProUGUI Title { get; private set; }
        [field: SerializeField] public TextMeshProUGUI Text { get; private set; }
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ButtonText { get; private set; }
    }

    public struct Message
    {
        public string Title;
        public string Text;
        public string ButtonText;
        public WindowNotify.DelegateWindowNotify Callback;
    }
}