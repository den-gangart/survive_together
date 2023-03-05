using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public enum TileSides
{
    Bottom,
    Right,
    Left,
    Top
}

public class MapElement
{

    public int id = 0;
    public MapElement[] tileSide = new MapElement[4];
    public int autotileID;

    public void AddTileToSide(TileSides side, MapElement tile)
    {
        tileSide[(int)side] = tile;
        CalculateTilesID();
    }

    public void RemoveSide(MapElement tile)
    {
        var total = tileSide.Length;
        for (var i = 0; i < total; i++)
        {
            if (tileSide[i] != null)
            {
                if (tileSide[i].id == tile.id)
                {
                    tileSide[i] = null;
                }
            }
        }
        CalculateTilesID();
    }

    public void ClearTileSide()
    {
        var total = tileSide.Length;
        for (var i = 0; i < total; i++)
        {
            var tile = tileSide[i];
            if (tile != null)
            {
                tile.RemoveSide(this);
                tileSide[i] = null;
            }
        }
        CalculateTilesID();
    }

    private void CalculateTilesID()
    {
        var sideValues = new StringBuilder();

        foreach (MapElement tile in tileSide)
        {
            sideValues.Append(tile == null ? "0" : "1");
        }
        autotileID = Convert.ToInt32(sideValues.ToString(), 2);
    }

}