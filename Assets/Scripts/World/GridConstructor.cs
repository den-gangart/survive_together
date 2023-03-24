using System.Collections.Generic;
using UnityEngine;

namespace SurviveTogether.World
{
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
                InitialTileByParams(go, tile, mapParam);

                if (column == (maxColumns - 1))
                {
                    row++;
                }
            }
        }

         void InitialTileByParams(GameObject currentTileObject, MapElement tile, MapParamInfo mapParam)
        {
            var spriteCollection = mapParam.spriteCollection[tile.tileTypeGroupId].SpritesList;
            var bgSr = currentTileObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            var sr = currentTileObject.GetComponent<SpriteRenderer>();

            if (tile.tileId != -1)
            {
                sr.sprite = GetSpriteWithCollection(spriteCollection, tile.tileId); // spriteCollection[tile.tileId];
            }
            if (tile.bgTileId != -1)
            {
                var grassCollection = mapParam.spriteCollection[(int)TileType.Grass].SpritesList;
                bgSr.sprite = GetSpriteWithCollection(grassCollection, tile.tileId); // grassCollection[tile.bgTileId];
            }
            if (tile.tileTypeGroupId > 15)
            {
                var currCollider = currentTileObject.AddComponent<BoxCollider2D>(); // TODO: added collider to brefabs & get component only in usible tiles
                currCollider.size = mapParam.tileSize;
                currCollider.isTrigger = tile.isInteractable;
            }
        }

         void ClearMapContainer(GameObject mapContainer)
        {
            var children = mapContainer.transform.GetComponentsInChildren<Transform>();
            var childrenLength = children.Length - 1;
            for (var i = childrenLength; i > 0; i--)
            {
                GameObject.Destroy(children[i].gameObject);
            }
        }

        Sprite GetSpriteWithCollection(List<Sprite> spriteCollection, int spriteId )
        {
            var collectionSize = spriteCollection.Count;

            return collectionSize <= spriteId ? spriteCollection[0] : spriteCollection[spriteId];
        }

    }
}