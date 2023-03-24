using SurviveTogether.World;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveTogether.Players
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField]
        private Collider2D triggerCollider;
        public GameObject triggerObj; 
        private Collider2D PlayerColliderCollider;

        private void Awake()
        {
            PlayerColliderCollider = GetComponent<BoxCollider2D>();
        }


        private void OnTriggerEnter2D(Collider2D triggerObj)
        {
            Debug.Log("SendEvent");
            EventSystem.Broadcast(new TileEnterEvent { name = triggerObj.gameObject.name });
        }

    }
}
