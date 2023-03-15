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
    Lake = 21
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

    public void CreateIsland(
        float erodePercent,
        int erodeIterations,
        float treePercent,
        float hillPercent,
        float mountainPercent,
        float townPercent,
        float monsterPercent,
        float lakePercent
        )
    {
        DecorateTiles(LandTiles, lakePercent, TileType.Empty);

        for (var i = 0; i < erodeIterations; i++)
        {
            DecorateTiles(CoastTiles, erodePercent, TileType.Empty);
        }

        var openTiles = LandTiles;
        RandomizeTileArray(openTiles);
        openTiles[0].tileTypeGroupId = (int)TileType.Castle;

        DecorateTiles(LandTiles, treePercent, TileType.Tree);
        DecorateTiles(LandTiles, hillPercent, TileType.Hills);
        DecorateTiles(LandTiles, mountainPercent, TileType.Mountains);
        DecorateTiles(LandTiles, townPercent, TileType.Towns);
        DecorateTiles(LandTiles, monsterPercent, TileType.Lake);
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

    public void DecorateTiles(MapElement[] tiles, float percent, TileType type)
    {
        var total = Mathf.FloorToInt(tiles.Length * percent);

        RandomizeTileArray(tiles);

        for (var i = 0; i < total; i++)
        {
            var tile = tiles[i];
            if (type == TileType.Empty)
            {
                tile.ClearTileSide();
            }
            tile.tileTypeGroupId = (int)type;
        }
    }

    public void RandomizeTileArray(MapElement[] tiles)
    {
        for (var i = 0; i < tiles.Length; i++)
        {
            var tmp = tiles[i];
            var r = Random.Range(i, tiles.Length);
            tiles[i] = tiles[r];
            tiles[r] = tmp;
        }
    }

}