using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Space]
    [Header("===== Map Objects Link =====")]
    public MapData mapData;
    public GameObject mapContainer;

    [Space]
    [Header("===== Map Dimensions =====")]
    public int mapWidth = 20;
    public int mapHeight = 20;

    [Space]
    [Header("===== Vizual Map Elements =====")]
    public GameObject tilePrefab;
    public Vector2 tileSize = new Vector2(16, 16);

    [Space]
    [Header("===== Map Sprites =====")]
    public Texture2D MapTexture;
    public Sprite[] spriteCollection;

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

    void Start()
    {
        map = new MapConstructor();
    }

    public void MakeMap()
    {
        map.NewMap(mapWidth, mapHeight);
        map.CreateIsland(
            erodePercent,
            erodeIterations,
            densityTreePlantations,
            hillsDensity,
            mountainDensity,
            townsDencity,
            cavesDencity,
            lakePercent
            );
        //Debug.Log("Created a new " + map.columns + "x" + map.rows + " map");
        CreateGrid(map);
        SetPositionToCastle(map.CastleTile.id);
    }

    public void LoadMap()
    {
        var becaupMapObj = mapData.RestorMapDataFromJson();
        CreateGrid(becaupMapObj);
    }

    public void SaveDataMap()
    {
        SaveMapToObj();
    }

    void SaveMapToObj()
    {
        mapData.SaveAndConvertToJsonData(map);
    }

    void CreateGrid(MapConstructor mapObject)
    {
        ClearMapContainer();
        // Sprite[] sprites = Resources.LoadAll<Sprite>(islandTexture.name);
   
        var total = mapObject.tiles.Length;
        var maxColumns = mapObject.columns;
        var row = 0;

        for (var i = 0; i < total; i++)
        {
            var column = i % maxColumns;

            var newX = column * tileSize.x;
            var newY = -row * tileSize.y;

            var go = Instantiate(tilePrefab);
            go.name = "Tile " + i;
            go.transform.SetParent(mapContainer.transform);
            go.transform.position = new Vector3(newX, newY, 0);

            var tile = mapObject.tiles[i];
            var spriteID = tile.autotileID;

            if (spriteID >= 0)
            {
                var sr = go.GetComponent<SpriteRenderer>();
                var tileConfigurator = go.GetComponent<TileConfiguration>(); // TODO: use tile method configeration
                sr.sprite = spriteCollection[spriteID];
            }

            if (column == (maxColumns - 1))
            {
                row++;
            }
        }
    }

    void ClearMapContainer()
    {
        var children = mapContainer.transform.GetComponentsInChildren<Transform>();
        for (var i = children.Length - 1; i > 0; i--)
        {
            Destroy(children[i].gameObject);
        }
    }

    void SetPositionToCastle(int index)
    {
        var camPosition = Camera.main.transform.position;
        var width = map.columns;
        camPosition.x = (index % width) * tileSize.x;
        camPosition.y = -((index / width) * tileSize.y);
        Camera.main.transform.position = camPosition;
    }

}
