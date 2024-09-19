using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private Door _door;
        [SerializeField] private WindowNotify _notify;
        [SerializeField] private Audio _audio;

        public override void InstallBindings()
        {
            Container.Bind<IPause>().To<Pause>().AsSingle();
            Container.BindInstance(_player).AsSingle();
            Container.BindInstance(_door).AsSingle();
            Container.BindInstance<IWindowNotify>(_notify).AsSingle();
            Container.BindInstance<IAudio>(_audio).AsSingle();
        }
    }
}