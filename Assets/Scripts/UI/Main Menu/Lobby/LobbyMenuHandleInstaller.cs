using UnityEngine;
using Zenject;

public class LobbyMenuHandleInstaller : MonoInstaller
{
    [SerializeField] private LobbyMenuHandler _lobbyHandler;

    public override void InstallBindings()
    {
        Container.Bind<LobbyMenuHandler>().FromInstance(_lobbyHandler).AsSingle();
    }
}