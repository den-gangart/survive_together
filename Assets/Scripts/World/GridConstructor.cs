using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridConstructor
{
   public void CreateGrid(MapConstructor mapObject, MapParamInfo mapParam, GameObject mapContainer)
    {
        ClearMapContainer(mapContainer);
        // Sprite[] sprites = Resources.LoadAll<Sprite>(islandTexture.name);

        var total = mapObject.tiles.Length;
        var maxColumns = mapObject.columns;
        var row = 0;

        for (var i = 0; i < total; i++)
        {
            var column = i % maxColumns;

            var newX = column * mapParam.tileSize.x;
            var newY = -row * mapParam.tileSize.y;

            var go = GameObject.Instantiate(mapParam.tilePrefab);
            go.name = "Tile " + i;
            go.transform.SetParent(mapContainer.transform);
            go.transform.position = new Vector3(newX, newY, 0);

            var tile = mapObject.tiles[i];
            var spriteID = tile.autotileID;

            if (spriteID >= 0)
            {
                var sr = go.GetComponent<SpriteRenderer>();
                //var tileConfigurator = go.GetComponent<TileConfiguration>(); 
                // TODO: use tile method configeration
                // tileConfigurator.TileConfig(spriteID, mapWidth, mapHeight);
                sr.sprite = mapParam.spriteCollection[spriteID];
            }

            if (column == (maxColumns - 1))
            {
                row++;
            }
        }
    }

    public void ClearMapContainer(GameObject mapContainer)
    {
        var children = mapContainer.transform.GetComponentsInChildren<Transform>();
        for (var i = children.Length - 1; i > 0; i--)
        {
            GameObject.Destroy(children[i].gameObject);
        }
    }


}
