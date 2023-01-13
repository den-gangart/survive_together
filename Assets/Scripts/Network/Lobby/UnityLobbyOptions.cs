using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;

public class UnityLobbyOptions: ILobbyOptions
{
    public JoinLobbyByCodeOptions GetJoinLobbyByCodeOptions()
    {
        return new JoinLobbyByCodeOptions { Player = GetPlayerInfo() };
    }

    public JoinLobbyByIdOptions GetJoinLobbyByIdOptions()
    {
        return new JoinLobbyByIdOptions { Player = GetPlayerInfo() };
    }

    public CreateLobbyOptions GetCreateLobbyOptions(bool isPrivate)
    {
        return new CreateLobbyOptions
        {
            Player = GetPlayerInfo(),
            IsPrivate = isPrivate,
            Data = new Dictionary<string, DataObject>()
            {
                { LobbyDataKeys.JOIN_CODE, new DataObject(DataObject.VisibilityOptions.Public) },
            },
        };
    }

    public UpdateLobbyOptions GetUpdateLobbyOptions(params LobbyCustomData[] lobbyCustomDataList)
    {
        Dictionary<string, DataObject> lobbyData = new Dictionary<string, DataObject>();
        
        foreach(var customData in lobbyCustomDataList)
        {
            lobbyData.Add(customData.key, new DataObject(DataObject.VisibilityOptions.Public, customData.value));
        }

        return new UpdateLobbyOptions
        {
            Data = lobbyData,
        };
    }

    public Player GetPlayerInfo()
    {
        string playerName = new PlayerNameData().PlayerName;

        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject>()
            {
                { PlayerDataKeys.PLAYER_NAME, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, playerName) },
            }
        };
    }
}
