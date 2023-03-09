using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using Unity.Collections;

namespace SurviveTogether.Players
{
    public class PlayerNameView : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        private NetworkVariable<FixedString32Bytes> _name = new NetworkVariable<FixedString32Bytes>(
            string.Empty, 
            NetworkVariableReadPermission.Everyone, 
            NetworkVariableWritePermission.Owner
        );


        public override void OnNetworkSpawn()
        {
            _name.OnValueChanged += OnNameChanged;
            base.OnNetworkSpawn();
        }

        private void Start()
        {
            if (IsOwner)
            {
                _name.Value = PlayerNameManager.Instance.PlayerName;
            }
        }

        private void OnNameChanged(FixedString32Bytes prevValue, FixedString32Bytes currentValue)
        {
            _textMesh.text = currentValue.ToString();
        }
    }
}