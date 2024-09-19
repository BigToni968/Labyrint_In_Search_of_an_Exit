using UnityEngine;
using Zenject;

namespace Game
{
    public class WindowMenu : MonoBehaviour
    {
        [SerializeField] private Canvas _self;

        private IPause _pause;

        [Inject]
        public void Constrcutor(IPause pause)
        {
            _pause = pause;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Show();
            }
        }

        public void Show()
        {
            Cursor.lockState = CursorLockMode.Confined;
            _pause.SetStatus(_self.enabled = true);
        }

        public void Hide()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _pause.SetStatus(_self.enabled = false);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
