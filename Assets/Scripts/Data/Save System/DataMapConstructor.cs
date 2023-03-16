using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveTogether.World;

namespace SurviveTogether.Data
{
    public class DataMapConstructor
    {
        private MapParamInfo _mapParam;
        private RandomMapParamData _randomMapParamData;

        public DataMapConstructor(RandomMapParamData randomMapParamData, MapParamInfo mapParam)
        {
            _randomMapParamData = randomMapParamData;
            _mapParam = mapParam;
        }

        public MapConstructor CreateMap()
        {
            MapConstructor map = new MapConstructor();
            map.NewMap(_randomMapParamData.mapWidth, _randomMapParamData.mapHeight);
            map.CreateMapByParams(
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
            return map;
        }
    }
}