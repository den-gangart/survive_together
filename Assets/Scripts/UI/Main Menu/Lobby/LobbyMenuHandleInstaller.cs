using UnityEngine;
using Zenject;

namespace SurviveTogether.UI
{
    public class LobbyMenuHandleInstaller : MonoInstaller
    {
        [SerializeField] private LobbyMenuHandler _lobbyHandler;

        public override void InstallBindings()
        {
            Container.Bind<LobbyMenuHandler>().FromInstance(_lobbyHandler).AsSingle();
        }
    }
}