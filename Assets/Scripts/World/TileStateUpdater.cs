using UnityEngine;

namespace SurviveTogether.World
{
    class TileStateUpdater : MonoBehaviour
    {
        [SerializeField]
        private MapParamInfo _mapParamInfoObj;
        private GameObject[] _tileObjectsArray;
        private MapConstructor _tilesData;

        private void Start()
        {
            _tilesData = _mapParamInfoObj.mapData.LoadFromJson();
            var _tilesArrayLenght = _tilesData.tiles.Length;
            _tileObjectsArray = new GameObject[_tilesArrayLenght];

            EventSystem.AddEventListener<TileEnterEvent>(TileUpdate);
        }

        public void AddTileToArray(GameObject TileObject)
        {
            var tileNumber = GetTileNumberWithName(TileObject.name);
            _tileObjectsArray[tileNumber] = TileObject;
        }

        public void TileUpdate(TileEnterEvent tileName)
        {
            var tileNumber = GetTileNumberWithName(tileName.name);
            var currentTile = _tilesData.tiles[tileNumber];
            Debug.Log("currentTile.id = " + currentTile.id + " currentTile.isInteractable = " + currentTile.isInteractable);

        }

         int GetTileNumberWithName(string tileObjectName)
        {
            int lastStringSymbol = tileObjectName.LastIndexOf(' ');
            string strTileNumber = tileObjectName.Substring(lastStringSymbol);

            var i = 0;
            return int.Parse(strTileNumber);
        }

         void OnDisable()
        {
            EventSystem.RemoveEventListener<TileEnterEvent>(TileUpdate);
        }
    }
}