using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using UnityEngine;
using System;

public class UnityLobbyProvider
{
    private Lobby _hostLobby;

    public bool IsOwner { get; private set; }
    public Lobby HostLobby { get { return _hostLobby; } }
    public event Action<Lobby> JoinedToLobby;
    public event Action LeftFromLobby;

    public async void CheckLobbyStatus()
    {
        try
        {
            await Lobbies.Instance.SendHeartbeatPingAsync(_hostLobby.Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    public async void CreateLobby(LobbyParameters lobbyParameters)
    {
        try
        {
            Lobby lobby =  await Lobbies.Instance.CreateLobbyAsync(lobbyParameters.name, lobbyParameters.maxPlayerCount, GetDefaultLobbyOptions(lobbyParameters.isPrivate));
            IsOwner = true;
            JoinedToLobby?.Invoke(lobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    public async Task<List<Lobby>> GetLobbyList()
    {
        var result =  await Lobbies.Instance.QueryLobbiesAsync();
        return result.Results;
    }

    public async void JoinLobbyByCode(string code)
    {
        try
        {
            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(code);
            JoinedToLobby?.Invoke(lobby);
            IsOwner = false;
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    public async void JoinLobbyById(string lobbyId)
    {
        try
        {
            Lobby lobby =  await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId);
            JoinedToLobby?.Invoke(lobby);
            IsOwner = false;
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    public void LeaveLobby()
    {
        try
        {
            LeftFromLobby?.Invoke();
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    private CreateLobbyOptions GetDefaultLobbyOptions(bool isPrivate)
    {
        return new CreateLobbyOptions
        {
            IsPrivate = isPrivate,
            Data = new Dictionary<string, DataObject>(),
        };
    }
}
