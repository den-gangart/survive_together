using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SurviveTogether.Network
{
    public interface IMultiplayerProviderWithCode
    {
        Task<string> StartGame();
        Task JoinGame(string joinCode);
    }
}