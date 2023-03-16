using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using SurviveTogether.Data;

namespace SurviveTogether.UI
{
    public class MapSelector : MonoBehaviour
    {
        [SerializeField] private Transform _listTransform;
        [SerializeField] private MapElementForList _existedMapElement;
        [SerializeField] private MapElementForList _newMapElement;

        [Inject] private LevelDataManager _dataManager;
        private List<MapElementForList> _mapElements;
        private MapElementForList _selectedElement;
        private const string DEFAULT_NAME = "NewMap";

        private void Start()
        {
            _mapElements = new List<MapElementForList>();
            List<LevelData> levelDataList = _dataManager.GetLevelDatas();
            SpawnMapList(levelDataList);
            SpawnNewMapField();

            _mapElements[0].Select();
        }

        public void FinishSelection()
        {
            int index = _mapElements.IndexOf(_selectedElement);

            if (index == _mapElements.Count - 1)
            {
                _dataManager.CreateNewLevel(_selectedElement.Name);
            }
            else
            {
                _dataManager.SelectCurrentLevel(index);
            }
        }

        private void SpawnMapList(List<LevelData> levelDataList)
        {
            foreach (var levelData in levelDataList)
            {
                MapElementForList mapElement = Instantiate(_existedMapElement, _listTransform);
                mapElement.InitizalizeAsExisted(levelData.name, levelData.creationDate, levelData.modifyDate);
                AddElement(mapElement);
            }
        }
        private void SpawnNewMapField()
        {
            MapElementForList mapElement = Instantiate(_newMapElement, _listTransform);
            mapElement.InitizalizeAsNew(DEFAULT_NAME);
            AddElement(mapElement);
        }

        private void AddElement(MapElementForList element)
        {
            element.ElementSelected += OnItemSelected;
            _mapElements.Add(element);
        }

        private void OnItemSelected(MapElementForList mapElement)
        {
            _selectedElement = mapElement;

            foreach(var element in _mapElements)
            {
                if (element != mapElement)
                {
                    element.Deselect();
                }
            }
        }
    }
}