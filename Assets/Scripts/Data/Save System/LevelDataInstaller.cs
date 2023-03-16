using UnityEngine;
using Zenject;

namespace SurviveTogether.Data
{
    public class LevelDataInstaller : MonoInstaller
    {
        [SerializeField] private MapParamInfo _mapParam;
        [SerializeField] private RandomMapParamData _randomMapParamData;

        public override void InstallBindings()
        {
            DataMapConstructor mapConstructor = new DataMapConstructor(_randomMapParamData, _mapParam);
            LevelDataManager levelData = new LevelDataManager(mapConstructor);
            NetworkLevelHandler levelHandler = new NetworkLevelHandler(levelData);

            Container.Bind<LevelDataManager>().FromInstance(levelData);
            Container.Bind<NetworkLevelHandler>().FromInstance(levelHandler);
        }
    }
}