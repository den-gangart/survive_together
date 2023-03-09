using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SurviveTogether.GameField
{
    [RequireComponent(typeof(Camera))]
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float _timeSpeed;
        private Transform _target;

        public void Start()
        {
            _target = NetworkManager.Singleton.LocalClient.PlayerObject.gameObject.transform;
        }

        private void FixedUpdate()
        {
            if (_target != null)
            {
                Vector3 nextPosition = Vector3.Lerp(transform.position, _target.position, _timeSpeed);
                nextPosition.z = transform.position.z;

                transform.position = nextPosition;
            }
        }
    }
}