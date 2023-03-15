using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridConstructor
{
   public void CreateGrid(MapConstructor mapObject, MapParamInfo mapParam, GameObject mapContainer)
    {
        ClearMapContainer(mapContainer);

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
            var spriteID = tile.tileTypeGroupId;

            if (spriteID >= 0)
            {
                var sr = go.GetComponent<SpriteRenderer>();
                var chSp = go.GetComponentInChildren<SpriteRenderer>();
                
                //var tileConfigurator = go.GetComponent<TileConfiguration>(); 
                // TODO: use tile method configeration
                var spriteCollection = mapParam.spriteCollection[spriteID].SpritesList;
                var randomSpriteId = Random.Range(0, spriteCollection.Count - 1);
                sr.sprite = spriteCollection[randomSpriteId];
               // chSp.sprite = spriteCollection[randomSpriteId];
               // sr.sprite = mapParam.spriteCollection[15].SpritesList[0];
                tile.TileId = randomSpriteId;
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
