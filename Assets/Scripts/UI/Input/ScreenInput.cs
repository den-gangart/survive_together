using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScreenInput : MonoBehaviour
{
    [Inject] private InputType _inputType;

    private void Awake()
    {
        if(_inputType == InputType.Devices)
        {
            Destroy(gameObject);
        }
    }
}
