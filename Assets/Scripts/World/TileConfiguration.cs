using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileConfiguration
{
    /* [SerializeField]
     private int TileId;
     [SerializeField]
     private GameObject TileColliderObj;

     private void Start()
     {
         TileColliderObj.AddComponent<BoxCollider2D>();
     }
    */

    public abstract void TileConfig();
}
