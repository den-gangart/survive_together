using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveTogether.GameField
{
    public class MoveCamera : MonoBehaviour
    {
        public float speed = 0.5f;

        private Vector3 startPosition;
        private bool moving;


        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                startPosition = Input.mousePosition;
                moving = true;
            }

            if (Input.GetMouseButtonUp(1) && moving)
            {
                moving = false;
            }

            if (moving)
            {
                Vector3 position = Camera.main.ScreenToViewportPoint(Input.mousePosition - startPosition);
                Vector3 move = new Vector3(position.x * speed, position.y * speed, 0);
                transform.Translate(move, Space.Self);
            }

        }
    }
}