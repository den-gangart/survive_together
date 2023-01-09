using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using Unity.Services.Authentication;
using UnityEngine;
using System;

public class UnityLobbyProvider
{
    private Lobby _hostLobby;

    public bool IsOwner { get; private set; }
    public Lobby HostLobby { get { return _hostLobby; } }
    public event Action<Lobby> JoinedToLobby;
    public event Action<Lobby> LobbyUpdate;
    public event Action LeftFromLobby;

    public async void HeartBeat()
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

    public async void UpdateLobby()
    {
        try
        {
            Lobby lobby =  await Lobbies.Instance.GetLobbyAsync(_hostLobby.Id);
            _hostLobby = lobby;
            LobbyUpdate?.Invoke(lobby);
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
            _hostLobby =  await Lobbies.Instance.CreateLobbyAsync(lobbyParameters.name, lobbyParameters.maxPlayerCount, GetCreateLobbyOptions(lobbyParameters.isPrivate));
            IsOwner = true;
            JoinedToLobby?.Invoke(_hostLobby);
            Debug.Log("Lobby created");
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
            Debug.Log("Joined to lobby");
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
            Debug.Log("Joined to lobby");
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
            _hostLobby = null;
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    public void RemoveLobby()
    {
        try
        {
            Lobbies.Instance.DeleteLobbyAsync(_hostLobby.Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    private CreateLobbyOptions GetCreateLobbyOptions(bool isPrivate)
    {
        return new CreateLobbyOptions
        {
            Player = GetPlayerInfo(),
            IsPrivate = isPrivate,
            Data = new Dictionary<string, DataObject>(),
        };
    }

    private Player GetPlayerInfo()
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject>()
            {
                { PlayerDataKeys.PLAYER_NAME, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, new PlayerNameData().PlayerName) },
            }
        };
    }
}
