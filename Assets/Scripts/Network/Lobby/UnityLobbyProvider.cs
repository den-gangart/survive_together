using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using Unity.Services.Authentication;
using UnityEngine;
using System;

public class UnityLobbyProvider
{
    public bool IsOwner { get; private set; }
    public bool IsJoinedToGame { get; private set; }
    public Lobby HostLobby { get { return _hostLobby; } }
    public event Action JoinToGameSession;


    private Lobby _hostLobby;
    private ILobbyOptions _lobbyOptions;
    private IMultiplayerProviderWithCode _multiplayerProvider;

    public UnityLobbyProvider(ILobbyOptions lobbyOptions, IMultiplayerProviderWithCode multiplayerProvider)
    {
        IsJoinedToGame = false;
        _lobbyOptions = lobbyOptions;
        _multiplayerProvider = multiplayerProvider;
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

    public async void CreateGameSession()
    {
        try
        {
            string _joinCode = await _multiplayerProvider.StartGame();
            LobbyCustomData lobbyCustomData = new LobbyCustomData(LobbyDataKeys.JOIN_CODE, _joinCode);
            UpdateLobbyData(null, lobbyCustomData);
            JoinToGameSession?.Invoke();
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    public async void JoinGameSession(string joinCode)
    {
        try
        {
            await _multiplayerProvider.JoinGame(joinCode);
            JoinToGameSession?.Invoke();
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    public async void GetLobby(Action<Lobby> lobbyUpdate)
    {
        try
        {
            Lobby lobby =  await Lobbies.Instance.GetLobbyAsync(_hostLobby.Id);
            _hostLobby = lobby;
            lobbyUpdate?.Invoke(lobby);

            if(!IsJoinedToGame)
            {
                CheckGameSessionStart();
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }

    public void CheckGameSessionStart()
    {
        string joinCode = _hostLobby.Data[LobbyDataKeys.JOIN_CODE].Value;
        if (IsOwner || string.IsNullOrEmpty(joinCode))
        {
            return;
        }

        JoinGameSession(joinCode);
        IsJoinedToGame = true;
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
