using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Zenject;
using SurviveTogether.Players.InputHandle;

namespace SurviveTogether.Players
{
    [RequireComponent(typeof(IPlayerView))]
    [RequireComponent(typeof(IPlayerMovement))]
    public class PlayerController : NetworkBehaviour
    {
        [Inject] private IPlayerInput _playerInput;
        private IPlayerView _playerView;
        private IPlayerMovement _playerMovement;


        private void Start()
        {
            _playerView = GetComponent<IPlayerView>();
            _playerMovement = GetComponent<IPlayerMovement>();
        }

        private void FixedUpdate()
        {
            if(IsOwner)
            {
               Vector2 velocity = _playerInput.GetMovementInput();
                _playerMovement.Move(velocity);
                _playerView.Move(velocity);
            }
        }
    }
}