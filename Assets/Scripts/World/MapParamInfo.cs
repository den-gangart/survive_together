using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Assets/Map Param Info")]
public class MapParamInfo : ScriptableObject
{
    [Space]
    [Header("===== Vizual Map Elements =====")]
    public GameObject tilePrefab;
    public Vector2 tileSize = new Vector2(16, 16);
    public MapData mapData;

    [Space]
    [Header("===== Map Sprites =====")]
    public Texture2D MapTexture;
    public Sprite[] spriteCollection;
}
