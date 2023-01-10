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
    private ILobbyOptions _lobbyOptions;

    public bool IsOwner { get; private set; }
    public Lobby HostLobby { get { return _hostLobby; } }

    public UnityLobbyProvider(ILobbyOptions lobbyOptions)
    {
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
        }
    }

    public async void UpdateLobby(Action<Lobby> lobbyUpdate)
    {
        try
        {
            Lobby lobby =  await Lobbies.Instance.GetLobbyAsync(_hostLobby.Id);
            _hostLobby = lobby;
            lobbyUpdate?.Invoke(lobby);

            if(lobby.Players.Count == 0)
            {
                RemoveLobby();
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    public async void CreateLobby(LobbyParameters lobbyParameters, Action<Lobby> joinedToLobby)
    {
        try
        {
            _hostLobby =  await Lobbies.Instance.CreateLobbyAsync(
                lobbyParameters.name, 
                lobbyParameters.maxPlayerCount, 
                _lobbyOptions.GetCreateLobbyOptions(lobbyParameters.isPrivate)
            );

            ProccessJoinToLobby(joinedToLobby, true);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    public async void LoadLobbyList(Action<List<Lobby>> lobbyListLoaded)
    {
        var result =  await Lobbies.Instance.QueryLobbiesAsync();
        lobbyListLoaded?.Invoke(result.Results);
    }

    public async void JoinLobbyByCode(string code, Action<Lobby> joinedToLobby)
    {
        try
        {
            _hostLobby = await Lobbies.Instance.JoinLobbyByCodeAsync(code, _lobbyOptions.GetJoinLobbyByCodeOptions());
            ProccessJoinToLobby(joinedToLobby, false);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    public async void JoinLobbyById(string lobbyId, Action<Lobby> joinedToLobby)
    {
        try
        {
            _hostLobby =  await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId, _lobbyOptions.GetJoinLobbyByIdOptions());
            ProccessJoinToLobby(joinedToLobby, false);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    public async void LeaveLobby(Action leftFromLobby)
    {
        try
        {
            await Lobbies.Instance.RemovePlayerAsync(_hostLobby.Id, AuthenticationService.Instance.PlayerId);
            leftFromLobby?.Invoke();
            _hostLobby = null;
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
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
        }
    }

    private void ProccessJoinToLobby(Action<Lobby> joinedToLobby, bool isOwner)
    {
        joinedToLobby?.Invoke(_hostLobby);
        this.IsOwner = isOwner;

        if(isOwner)
        {
            Debug.Log("Lobby has created");
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
