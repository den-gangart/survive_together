using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;

public class PlayerLobbyComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerNameText;

    public void InitizalizePlayerInfo(Player player)
    {
        PlayerDataObject playerData;
        if (player.Data.TryGetValue(PlayerDataKeys.PLAYER_NAME, out playerData))
        {
            _playerNameText.text = playerData.Value;
        }
    }
}
