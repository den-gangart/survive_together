using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveTogether.Data;
using Zenject;
using System;
using SurviveTogether.World;

namespace SurviveTogether.Data
{
    public class LevelDataManager
    {
        public bool IsNewLevel { get { return _isNewLevel; } }
        public PlayerDataContainer PlayerDatas { get { return _currentLevel.playerDatas; } }

        private LevelData _currentLevel;
        private LevelDataSaver _dataSaver;
        private List<LevelData> _levelDatas;
        private bool _isNewLevel;

        private DataMapConstructor _mapConstructor;

        public LevelDataManager(DataMapConstructor mapConstructor)
        {
            _dataSaver = new LevelDataSaver();
            _levelDatas = _dataSaver.LevelDatas;
            _mapConstructor = mapConstructor;
        }

        public List<LevelData> GetLevelDatas() => _levelDatas;
        public LevelData GetCurrentLevel() => _currentLevel;
        public string GetCurrentLevelInJson() => JsonUtility.ToJson(_currentLevel);
        public void ParseLevelFromJson(string level) => _currentLevel = JsonUtility.FromJson<LevelData>(level);

        public void SaveLevel()
        {
            _currentLevel.modifyDate = DateTime.Now;

            if (_isNewLevel)
            {
                _dataSaver.AddLevel(_currentLevel);
            }
            else
            {
                _dataSaver.SaveLevel(_currentLevel);
            }

            _isNewLevel = false;
        }

        public void SelectCurrentLevel(int index)
        {
            _currentLevel = _levelDatas[index];
            _isNewLevel = false;
        }

        public void CreateNewLevel(string name)
        {
            LevelData levelData = new LevelData()
            {
                index = LevelDataSaver.DEFAULT_LEVEL_INDEX,
                name = name,
                creationDate = DateTime.Now,
                modifyDate = DateTime.Now,
                playerDatas = new PlayerDataContainer(),
            };

            _currentLevel = levelData;
            _isNewLevel = true;
        }

        public void SetMap(MapConstructor mapData)
        {
            _currentLevel.mapData = mapData;
        }

        public void GenerateMap()
        {
            _currentLevel.mapData = _mapConstructor.CreateMap();
        }
    }
}