using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Popup : MonoBehaviour
{
    public Action<Popup> PopupClosed;

    public virtual void ClosePopup()
    {
        PopupClosed?.Invoke(this);
    }
}
