using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputMovement : NetworkBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(IsOwner)
        {
            MoveByInput();
        }
    }

    private void MoveByInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 vectorMove = new Vector2(horizontalInput, verticalInput) * _speed;
        _rigidbody.velocity = vectorMove;
    }
}
