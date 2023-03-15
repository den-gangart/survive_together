using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Assets/Random Map Param Data")]
public class RandomMapParamData : ScriptableObject
{
    [Space]
    [Header("===== Map Dimensions =====")]
    public int mapWidth = 20;
    public int mapHeight = 20;

    [Space]
    [Header("===== Decorate Map =====")]
    [Range(0, .9f)]
    public float erodePercent = .5f;
    public int erodeIterations = 2;
    [Range(0, .9f)]
    public float densityTreePlantations = .3f;
    [Range(0, .9f)]
    public float hillsDensity = .2f;
    [Range(0, .9f)]
    public float mountainDensity = .1f;
    [Range(0, .9f)]
    public float townsDencity = .05f;
    [Range(0, .9f)]
    public float cavesDencity = .1f;
    [Range(0, .9f)]
    public float lakePercent = .05f;
}