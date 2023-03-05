using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;

public class PlayerLobbyComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerNameText;
    [SerializeField] private TextMeshProUGUI _ownerStatusText;

    public void InitizalizePlayerInfo(Player player, bool isOwner)
    {
        PlayerDataObject playerData;

        if (player.Data.TryGetValue(PlayerDataKeys.PLAYER_NAME, out playerData))
        {
            _playerNameText.text = playerData.Value;
        }

        _ownerStatusText.gameObject.SetActive(isOwner);
    }
}
