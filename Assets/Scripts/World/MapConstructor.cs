using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TileType
{
    Empty = 22,
    Grass = 15,
    Tree = 16,
    Hills = 17,
    Mountains = 18,
    Towns = 19,
    Castle = 20,
    Cave = 21,
}

[System.Serializable]
public class MapConstructor
{
    public MapElement[] tiles;
    public int columns;
    public int rows;

    public MapElement[] CoastTiles
    {
        get
        {
            return tiles.Where(t => t.tileTypeGroupId < (int)TileType.Grass).ToArray();
        }
    }

    public MapElement[] LandTiles
    {
        get
        {
            return tiles.Where(t => t.tileTypeGroupId == (int)TileType.Grass).ToArray();
        }
    }

    public MapElement CastleTile
    {
        get
        {
            return tiles.FirstOrDefault(t => t.tileTypeGroupId == (int)TileType.Castle);
        }
    }

    public void NewMap(int width, int height)
    {
        columns = width;
        rows = height;

        tiles = new MapElement[columns * rows];

        CreateTiles();
    }

    public void CreateMapByParams(
        float erodePercent,
        int erodeIterations,
        float treePercent,
        float hillPercent,
        float mountainPercent,
        float townPercent,
        float CavePercent,
        float lakePercent,
        MapParamInfo mapParam
        )
    {
        DecorateTiles(LandTiles, lakePercent, TileType.Empty, mapParam);

        for (var i = 0; i < erodeIterations; i++)
        {
            DecorateTiles(CoastTiles, erodePercent, TileType.Empty, mapParam);
        }

        var openTiles = LandTiles;
        RandomizeTileArray(openTiles);
        openTiles[0].tileTypeGroupId = (int)TileType.Castle;

        DecorateTiles(LandTiles, treePercent, TileType.Tree, mapParam);
        DecorateTiles(LandTiles, hillPercent, TileType.Hills, mapParam);
        DecorateTiles(LandTiles, mountainPercent, TileType.Mountains, mapParam);
        DecorateTiles(LandTiles, townPercent, TileType.Towns, mapParam);
        DecorateTiles(LandTiles, CavePercent, TileType.Cave, mapParam);

        /* Empty = 22,
           Tree = 16,
           Hills = 17,
           Mountains = 18,
           Towns = 19,
           Castle = 20,
           Cave = 21, */
    }

    private void CreateTiles()
    {
        var total = tiles.Length;
        for (var i = 0; i < total; i++)
        {
            var tile = new MapElement
            {
                id = i
            };
            tiles[i] = tile;
        }

        FindRightTileSides();
    }

    private void FindRightTileSides()
    {
        for (var curRow = 0; curRow < rows; curRow++)
        {
            for (var curColumn = 0; curColumn < columns; curColumn++)
            {
                var tile = tiles[columns * curRow + curColumn];

                if (curRow < rows - 1) // Bottom Sides
                {
                    tile.AddTileToSide(TileSides.Bottom, tiles[columns * (curRow + 1) + curColumn]);
                }

                if (curColumn < columns - 1) // Right Sides
                {
                    tile.AddTileToSide(TileSides.Right, tiles[columns * curRow + curColumn + 1]);
                }

                if (curColumn > 0) // Left Sides
                {
                    tile.AddTileToSide(TileSides.Left, tiles[columns * curRow + curColumn - 1]);
                }

                if (curRow > 0) // Top Sides
                {
                    tile.AddTileToSide(TileSides.Top, tiles[columns * (curRow - 1) + curColumn]);
                }

            }
        }
    }

    public void DecorateTiles(MapElement[] tiles, float percent, TileType type, MapParamInfo mapParam)
    {
        var total = Mathf.FloorToInt(tiles.Length * percent);

        RandomizeTileArray(tiles);

        for (var i = 0; i < total; i++)
        {
            tiles[i] = DecorateTileParams(tiles[i], type, mapParam); 
            
        }
      /*   public int id = 0;
    [NonSerialized] public MapElement[] tileSide = new MapElement[4];
    public int tileTypeGroupId;
    public int tileId;
    public int bgTileId;
    public bool isUsabl;
    public bool isInteractable;*/
}

     void RandomizeTileArray(MapElement[] tiles)
    {
        for (var i = 0; i < tiles.Length; i++)
        {
            var tmp = tiles[i];
            var r = Random.Range(i, tiles.Length);
            tiles[i] = tiles[r];
            tiles[r] = tmp;
        }
    }

    MapElement DecorateTileParams(MapElement tile,TileType type, MapParamInfo mapParam)
    {
       
        var prefBgTileId = -1;
        var prefisInteractable = false;

        tile.tileTypeGroupId = (int)type;
      
        var spriteCollection = mapParam.spriteCollection[tile.tileTypeGroupId].SpritesList;
        var prefTileId = spriteCollection.Count > 0 ? Random.Range(0, spriteCollection.Count - 1): 0;

        var grassCollection = mapParam.spriteCollection[(int)TileType.Grass].SpritesList;
        prefBgTileId = grassCollection.Count > 0 ? Random.Range(0, grassCollection.Count -1): 0;

      

        switch (type)
        {
            case TileType.Empty:
                tile.ClearTileSide();
                prefTileId = -1;
                prefBgTileId = -1;
                break;
            case TileType.Grass:
                prefBgTileId = -1;
                break;
            case TileType.Tree:
                prefisInteractable = true;
                break;
            case TileType.Hills:
                break;
            case TileType.Mountains:
                break;
            case TileType.Towns:
                prefisInteractable = true;
                break;
            case TileType.Castle:
                prefisInteractable = true;
                break;
            case TileType.Cave:
                prefisInteractable = true;
                break;
            default:
                break;
        }
       
        
        tile.tileId = prefTileId; 
        tile.bgTileId = prefBgTileId;
        tile.isUsabl = false;
        tile.isInteractable = prefisInteractable;

        return tile;
    }

}