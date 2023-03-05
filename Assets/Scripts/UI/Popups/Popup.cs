using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Popup : MonoBehaviour
{
    public event Action<Popup> PopupClose;

    protected virtual void OnStart() { }

    public virtual void ClosePopup()
    {
        PopupClose?.Invoke(this);
    }

    public virtual void Close() { }
    public virtual void Open() { }
}
