using System.Collections.Generic;
using UnityEngine;

namespace SurviveTogether.World
{
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
        public List<serializableListSprite> spriteCollection = new List<serializableListSprite>();
    }

    [System.Serializable]
    public class serializableListSprite
    {
        public List<UnityEngine.Sprite> SpritesList;
    }
}