using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [Space]
    [Header("===== Map Objects Link =====")]
    public GameObject mapContainer;
    public MapParamInfo mapParam;

    public RandomMapParamData randomMapParamData;

    public MapConstructor map;
    public GridConstructor grid;

    void Start()
    {
        LoadMap();

       // MakeMap();
    }

     void LoadMap()
    {
        map = new MapConstructor();
        grid = new GridConstructor();
        var becaupMapObj = mapParam.mapData.LoadFromJson();
        grid.CreateGrid(becaupMapObj, mapParam, mapContainer);

    }


    public MapConstructor MakeMap()
       {
        map.NewMap(randomMapParamData.mapWidth, randomMapParamData.mapHeight);
        map.CreateMapByParams(
           randomMapParamData.erodePercent,
           randomMapParamData.erodeIterations,
          randomMapParamData.densityTreePlantations,
           randomMapParamData.hillsDensity,
           randomMapParamData.mountainDensity,
           randomMapParamData.townsDencity,
           randomMapParamData.cavesDencity,
           randomMapParamData.lakePercent,
           mapParam
           );
        return map;
      }
}
