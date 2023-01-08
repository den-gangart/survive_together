using UnityEngine;
using System;

public class Singleton<T>: MonoBehaviour where T: Singleton<T>
{
    [SerializeField] private bool _isPermanent;
    private static T _instance;

    public static T Instance
    {
        get
        { 
            if (_instance != null)
            {
                return _instance;
            }

            throw new InvalidOperationException();
        }
    }

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = (T) this;
            if (_isPermanent)
            {
                DontDestroyOnLoad(this);
            }
            OnAwake();
        }
        else if (_instance != this)
        {
            Destroy(this);
        }
    }

    protected virtual void OnAwake() { }
}
