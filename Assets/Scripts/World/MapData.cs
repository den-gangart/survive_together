using UnityEngine;

namespace SurviveTogether.World
{
    [CreateAssetMenu(menuName = "Custom Assets/Map Data")]
    public class MapData : ScriptableObject
    {
        [Space]
        [Header("===== Json Data Field =====")]
        public string jsonDataMap;
        [SerializeField] private MapConstructor _map;

        public void SaveAndConvertToJsonData(MapConstructor map)
        {
            jsonDataMap = JsonUtility.ToJson(map);
        }

        public MapConstructor LoadFromJson()
        {
            MapConstructor mapConstructor = JsonUtility.FromJson<MapConstructor>(jsonDataMap); ;
            return mapConstructor;
        }
    }
}
