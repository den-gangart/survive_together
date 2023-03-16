using UnityEngine;
using SurviveTogether.Data;
using Zenject;

namespace SurviveTogether.World
{
    public class LevelLoader: MonoBehaviour
    {
        [Space]
        [Header("===== Map Objects Link =====")]
        [SerializeField] private GameObject _mapContainer;
        [SerializeField] private MapParamInfo _mapParam;
        [SerializeField] private RandomMapParamData _randomMapParamData;

        [SerializeField] private MapConstructor _map;
        [SerializeField] private GridConstructor _grid;

        [Inject] LevelDataManager _dataManager;

        public void LoadMap()
        {
            _map = _dataManager.GetCurrentLevel().mapData;
            _grid = new GridConstructor();

            _grid.CreateGrid(_map, _mapParam, _mapContainer);
        }


        public MapConstructor MakeMap()
        {
            _map.NewMap(_randomMapParamData.mapWidth, _randomMapParamData.mapHeight);
            _map.CreateMapByParams(
               _randomMapParamData.erodePercent,
               _randomMapParamData.erodeIterations,
              _randomMapParamData.densityTreePlantations,
               _randomMapParamData.hillsDensity,
               _randomMapParamData.mountainDensity,
               _randomMapParamData.townsDencity,
               _randomMapParamData.cavesDencity,
               _randomMapParamData.lakePercent,
               _mapParam
               );
            return _map;
        }
    }
}
