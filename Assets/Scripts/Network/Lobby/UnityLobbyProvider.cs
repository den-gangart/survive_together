using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using Unity.Services.Authentication;
using UnityEngine;
using System;

public class UnityLobbyProvider
{
    public event Action<Lobby> JoinedToLobby;
    public event Action<Lobby> LobbyUpdate;
    public event Action<List<Lobby>> LoadedLobbyList;
    public event Action<string> JoinToGameSession;

    public bool IsOwner { get; private set; }
    public bool IsJoinedToGame { get; private set; }
    public Lobby HostLobby { get { return _hostLobby; } }
    public bool IsJoinedToLobby { get { return _hostLobby != null; } }

    private Lobby _hostLobby;
    private ILobbyOptions _lobbyOptions;

    public UnityLobbyProvider(ILobbyOptions lobbyOptions)
    {
        IsJoinedToGame = false;
        _lobbyOptions = lobbyOptions;
    }

    public async void HeartBeat()
    {
        try
        {
            await Lobbies.Instance.SendHeartbeatPingAsync(_hostLobby.Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
            throw e;
        }
    }

    public async void UpdateLobby()
    {
        try
        {
            Lobby lobby =  await Lobbies.Instance.GetLobbyAsync(_hostLobby.Id);
            _hostLobby = lobby;
            LobbyUpdate?.Invoke(lobby);

            if(!IsJoinedToGame)
            {
                CheckGameSessionStart();
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
            throw e;
        }
    }

    public void CheckGameSessionStart()
    {
        string joinCode = _hostLobby.Data[LobbyDataKeys.JOIN_CODE].Value;
        IsJoinedToGame = !IsOwner && !string.IsNullOrEmpty(joinCode);

        if(IsJoinedToGame)
        {
            JoinToGameSession?.Invoke(joinCode);
        }
    }

    public async Task<Lobby> CreateLobby(LobbyParameters lobbyParameters)
    {
        try
        {
            _hostLobby =  await Lobbies.Instance.CreateLobbyAsync(
                lobbyParameters.name, 
                lobbyParameters.maxPlayerCount, 
                _lobbyOptions.GetCreateLobbyOptions(lobbyParameters.isPrivate)
            );

            ProccessJoinToLobby(true);

            return _hostLobby;
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
            throw e;
        }
    }

    public async Task<List<Lobby>> LoadLobbyList()
    {
        var result =  await Lobbies.Instance.QueryLobbiesAsync();
        return result.Results;
    }

    public async Task<Lobby> JoinLobbyByCode(string code)
    {
        try
        {
            _hostLobby = await Lobbies.Instance.JoinLobbyByCodeAsync(code, _lobbyOptions.GetJoinLobbyByCodeOptions());
            ProccessJoinToLobby(false);
            return _hostLobby;
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
            throw e;
        }
    }

    public async Task<Lobby> JoinLobbyById(string lobbyId)
    {
        try
        {
            _hostLobby =  await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId, _lobbyOptions.GetJoinLobbyByIdOptions());
            ProccessJoinToLobby(false);
            return _hostLobby;
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
            throw e;
        }
    }

    public void SetGameJoinCode(string code)
    {
        LobbyCustomData lobbyCustomData = new LobbyCustomData(LobbyDataKeys.JOIN_CODE, code);
        UpdateLobbyData(null, lobbyCustomData);
    }

    public async void UpdateLobbyData(Action<Lobby> lobbyUpdate, params LobbyCustomData[] lobbyCustomDatas)
    {
        try
        {
            Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(_hostLobby.Id, _lobbyOptions.GetUpdateLobbyOptions(lobbyCustomDatas));
            _hostLobby = lobby;
            lobbyUpdate?.Invoke(lobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
            throw e;
        }
    }

    public async void LeaveLobby()
    {
        try
        {
            if(_hostLobby.Players.Count == 1)
            {
                RemoveLobby();
            }
            else
            {
                await Lobbies.Instance.RemovePlayerAsync(_hostLobby.Id, AuthenticationService.Instance.PlayerId);
            }
          
            _hostLobby = null;
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
            throw e;
        }
    }

    public async void RemoveLobby()
    {
        try
        {
           await Lobbies.Instance.DeleteLobbyAsync(_hostLobby.Id);
           ResetLobbyData();
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
            throw e;
        }
    }

    private void ProccessJoinToLobby(bool isOwner)
    {
        JoinedToLobby?.Invoke(_hostLobby);
        this.IsOwner = isOwner;

        if (isOwner)
        { 
            Debug.Log("Lobby created");
            return;
        }

        Debug.Log("Joined to lobby");
    }

    private void ResetLobbyData()
    {
        _hostLobby = null;
        IsOwner = false;
    }
}
