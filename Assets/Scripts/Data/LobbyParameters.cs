using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LobbyParameters
{
    public string name;
    public int maxPlayerCount;
    public bool isPrivate;

    public LobbyParameters(string name, int maxPlayerCount, bool isPrivate)
    {
        this.name = name;
        this.maxPlayerCount = maxPlayerCount;
        this.isPrivate = isPrivate;
    }
}
