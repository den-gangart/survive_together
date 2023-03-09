using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Assets/Map Data")]
public class MapData : ScriptableObject
{
    [Space]
    [Header("===== Json Data Field =====")]
    public string jsonDataMap;
    public SavebleMapData _mapData;
   
    public void SaveAndConvertToJsonData(MapConstructor map)
    {
        var mapTilesArrObj = map.tiles;

        _mapData = new SavebleMapData()
        {
            mapHeight = map.rows,
            mapWidth = map.columns,
            tiles = new TileData[mapTilesArrObj.Length],
        };


        for (var i= 0; i < mapTilesArrObj.Length; i++)
        {
            _mapData.tiles[i] = new TileData()
            {
                index = mapTilesArrObj[i].autotileID,
                isUsed = false,
            };
        }

        jsonDataMap = JsonUtility.ToJson(_mapData);
    }

    public MapConstructor LoadFromJson()
    {
        _mapData = JsonUtility.FromJson<SavebleMapData>(jsonDataMap);

        MapConstructor mapConstructor = new MapConstructor()
        {
            columns = _mapData.mapWidth,
            rows = _mapData.mapHeight,
            tiles = new MapElement[_mapData.tiles.Length],
        };

        for (var i = 0; i < _mapData.tiles.Length; i++)
        {
            mapConstructor.tiles[i] = new MapElement()
            {
                autotileID = _mapData.tiles[i].index,
            };
        }

        return mapConstructor;
    }
}

[System.Serializable]
public class SavebleMapData
{
    public int mapWidth;
    public int mapHeight;
    public TileData[] tiles;
}

[System.Serializable]
public class TileData
{
    public int index;
    public bool isUsed;
}