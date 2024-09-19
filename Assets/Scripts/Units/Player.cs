using UnityEngine.SceneManagement;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Player : Unit, IItemUp, IDoorOpener, IDamageable
    {
        [SerializeField] private PlayerData _data;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _camera;
        [SerializeField] private AudioClip _damageSound;
        [SerializeField] private AudioClip _keyUpSound;
        [SerializeField] private PlayerModel _model;

        private float xRotation = 0;
        private float _runmultiplier = 1f;

        public event IItemUpCallback.ItemUpDelegate ItemUpCallback;

        private IWindowNotify _notify;
        private IAudio _audio;

        [Inject]
        public void Constrcutor(IWindowNotify notify, IAudio audio)
        {
            _notify = notify;
            _audio = audio;
        }

        public override void OnInit()
        {
            base.OnInit();
            _model = _data.Model.Clone() as PlayerModel;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Run()
        {
            _runmultiplier = Input.GetKey(KeyCode.LeftShift) ? _model.SpeedRunMultiplier : 1f;
        }

        private void Move()
        {
            Vector3 velocity = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")).normalized;
            _rigidbody.velocity = _model.SpeedWalk * _runmultiplier * Time.deltaTime * velocity;

            if (!_audio.Sound.isPlaying && velocity != Vector3.zero)
            {
                _audio.Sound.Play();
            }
        }

        private void Rotate()
        {
            float mouseX = Input.GetAxis("Mouse X") * _model.CameraSetting.Sensitivity.x * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _model.CameraSetting.Sensitivity.y * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, _model.CameraSetting.ClampEulers.x, _model.CameraSetting.ClampEulers.y);

            _camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }

        public void Take()
        {
            _model.AmountKey += 1;
            ItemUpCallback?.Invoke(_model.AmountKey);
            _audio.Sound.PlayOneShot(_keyUpSound);
        }

        public int Kyes()
        {
            return _model.AmountKey;
        }

        public void Hit(float Damage)
        {
            _model.Health -= Damage;
            _model.Health = Mathf.Clamp(_model.Health, 0, int.MaxValue);
            _audio.Sound.PlayOneShot(_damageSound);

            if (_model.Health <= 0f)
            {
                _notify.Show(new()
                {
                    Title = "You're dead...",
                    Text = "I'm sorry, but you're dead. But don't be sad because the adventure has only just begun." +
                    " Perhaps you wanted to be reborn again? But you won't remember anything from this life. ",
                    ButtonText = "Reincarnation",
                    Callback = () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex)
                });
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (_model.Health <= 0)
            {
                return;
            }

            Run();
            Move();
            Rotate();
        }

    }
}
