using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using SurviveTogether.Data;

namespace SurviveTogether.Network
{
    public interface ILobbyOptions
    {
        JoinLobbyByCodeOptions GetJoinLobbyByCodeOptions();
        JoinLobbyByIdOptions GetJoinLobbyByIdOptions();
        CreateLobbyOptions GetCreateLobbyOptions(bool isPrivate);
        UpdateLobbyOptions GetUpdateLobbyOptions(params LobbyCustomData[] lobbyCustomData);
        Player GetPlayerInfo();
    }
}