using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Zenject;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [Inject] private DiContainer _diContainer;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //  var go = _diContainer.InstantiatePrefab(_playerPrefab);
        // GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
    }
}
