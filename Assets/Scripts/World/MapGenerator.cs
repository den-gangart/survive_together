using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Space]
    [Header("===== Map Objects Link =====")]
    public GameObject mapContainer;
    public MapParamInfo mapParam;

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

    public MapConstructor map;
    public GridConstructor grid;

    void Start()
    {
        map = new MapConstructor();
        grid = new GridConstructor();
    }

    public void MakeMap()
    {
        map.NewMap(mapWidth, mapHeight);
        map.CreateMapByParams(
            erodePercent,
            erodeIterations,
            densityTreePlantations,
            hillsDensity,
            mountainDensity,
            townsDencity,
            cavesDencity,
            lakePercent,
            mapParam
            );
        grid.CreateGrid(map, mapParam, mapContainer);
        SetPositionToCastle(map.CastleTile.id);
    }

    public void LoadMap()
    {
        var becaupMapObj = mapParam.mapData.LoadFromJson();
        grid.CreateGrid(becaupMapObj, mapParam, mapContainer);
    }

    public void SaveDataMap()
    {
        SaveMapToObj();
    }

    void SaveMapToObj()
    {
        mapParam.mapData.SaveAndConvertToJsonData(map);
    }

    void SetPositionToCastle(int index)
    {
        var camPosition = Camera.main.transform.position;
        var width = map.columns;
        camPosition.x = (index % width) * mapParam.tileSize.x;
        camPosition.y = -((index / width) * mapParam.tileSize.y);
        Camera.main.transform.position = camPosition;
    }

}
