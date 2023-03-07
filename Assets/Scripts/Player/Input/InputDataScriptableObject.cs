using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Custom Assets/InputData")]
public class InputDataScriptableObject : ScriptableObjectInstaller
{
    [SerializeField] private InputType _inputType;
    private IPlayerInput _playerInput;

    public InputType InputType { get { return _inputType; } }
    public IPlayerInput PlayerInput { get { return _playerInput; } }

    public override void InstallBindings()
    {
#if UNITY_EDITOR
        _playerInput = _inputType == InputType.Devices ? new PlayerPCInput() : new PlayerMobileInput();
#else
        if (Application.isMobilePlatform)
        {
            SetMobileParameters();
        }
        else
        {
            SetPCParameters();
        }
#endif
        Container.Bind<InputType>().FromInstance(_inputType).AsSingle();
        Container.Bind<IPlayerInput>().FromInstance(_playerInput).AsSingle();
    }

    private void SetMobileParameters()
    {
        _inputType = InputType.Screen;
        _playerInput = new PlayerMobileInput();
    }

    private void SetPCParameters()
    {
        _inputType = InputType.Devices;
        _playerInput = new PlayerPCInput();
    }
}
