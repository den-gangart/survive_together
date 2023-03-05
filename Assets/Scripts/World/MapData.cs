using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Assets/Map Data")]
public class MapData : ScriptableObject
{
    [Space]
    [Header("===== Json Data Field =====")]
    public string jsonDataMap;

    public int mapWidth;
    public int mapHeight;
    public int mapMaxColumns;
    public int[] tileMapIndexesList;
    [HideInInspector]
    public object becaupMapObj;
   
    public void SaveAndConvertToJsonData(MapConstructor map)
    {
        mapHeight = map.columns;
        mapWidth = map.rows;
        mapMaxColumns = map.columns;
        becaupMapObj = map;

        var mapTilesArrObj = map.tiles;
        tileMapIndexesList = new int[mapTilesArrObj.Length];
        for (var i= 0; i < mapTilesArrObj.Length; i++)
        {
            tileMapIndexesList[i] = mapTilesArrObj[i].autotileID;
        }

        jsonDataMap = JsonUtility.ToJson(this, true);
    }

    public MapConstructor RestorMapDataFromJson()
    {
        becaupMapObj = JsonUtility.FromJson<MapConstructor>(jsonDataMap);
        return (MapConstructor)becaupMapObj;
    }
}
