using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [Space]
    [Header("===== Map Objects Link =====")]
    public GameObject mapContainer;
    public MapParamInfo mapParam;

    public MapConstructor map;
    public GridConstructor grid;

    void Start()
    {
        map = new MapConstructor();
        grid = new GridConstructor();
        var becaupMapObj = mapParam.mapData.RestorMapDataFromJson();
        grid.CreateGrid(becaupMapObj, mapParam, mapContainer);
    }
}
