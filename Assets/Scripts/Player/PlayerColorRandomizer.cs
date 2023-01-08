using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerColorRandomizer : NetworkBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private NetworkVariable<Color> _imageColor = new NetworkVariable<Color>(Color.red, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnNetworkSpawn()
    {
        _spriteRenderer.color = _imageColor.Value;
        _imageColor.OnValueChanged += OnColorChanged;
        base.OnNetworkSpawn();
    }

    public override void OnNetworkDespawn()
    {
        _imageColor.OnValueChanged -= OnColorChanged;
        base.OnNetworkDespawn();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space) && IsOwner)
        {
            _imageColor.Value = new Color(Random.value, Random.value, Random.value);
        }
    }

    private void OnColorChanged(Color previousValue, Color currentValue)
    {
        _spriteRenderer.color = _imageColor.Value;
    }
}
