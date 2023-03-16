using UnityEngine;
using Zenject;
using SurviveTogether.World;

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

            Container.Bind<LevelDataManager>().FromInstance(levelData);
        }
    }
}