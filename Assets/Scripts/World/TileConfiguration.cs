using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileConfiguration : MonoBehaviour
{
    [SerializeField]
    private int TileId;
    [SerializeField]
    private GameObject TileColliderObj;

    private void Start()
    {
        TileColliderObj.AddComponent<BoxCollider2D>();
    }
    public void TileConfig(int id, float xTileSize, float yTileSize)
    {
        TileId = id;
        var collider = TileColliderObj.GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(xTileSize, yTileSize);
    }
}
