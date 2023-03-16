using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SurviveTogether.Data
{
    public class LevelDataSaver
    {
        public int LevelCount { get { return _levelCount; } }
        public List<LevelData> LevelDatas { get { return _levelDatas; } }
        public const int DEFAULT_LEVEL_INDEX = -1;

        private int _levelCount;
        private List<LevelData> _levelDatas;


        public LevelDataSaver() 
        {
            _levelCount = PlayerPrefs.GetInt(PlayerPrefsKeys.LEVEL_COUNT, 0);
            _levelDatas = new List<LevelData>();

            if(_levelCount > 0)
            {
                LoadLevelList();
            }
        }

        public void SaveLevel(LevelData level)
        {
            string jsonLevel = JsonUtility.ToJson(level);
            PlayerPrefs.SetString(GetLevelKey(level.index), jsonLevel);
        }

        public void AddLevel(LevelData level)
        {
            UpdateLevelCount(_levelCount + 1);

            int levelIndex = _levelCount - 1;
            level.index = levelIndex;
            string jsonLevel = JsonUtility.ToJson(level);
            PlayerPrefs.SetString(GetLevelKey(levelIndex), jsonLevel);
        }

        public void RemoveLevel(LevelData level)
        {
            int levelIndex = level.index;

            if (levelIndex < _levelCount - 1)
            {
                MoveLevelsDataLeft(levelIndex);
            }
            else
            {
                PlayerPrefs.DeleteKey(GetLevelKey(_levelCount - 1));
            }

            UpdateLevelCount(_levelCount - 1);
        }

        public void ClearAllData()
        {
            if (_levelCount == 0)
            {
                return;
            }

            PlayerPrefs.SetInt(PlayerPrefsKeys.LEVEL_COUNT, 0);
            for (int i = 0; i < _levelCount; i++)
            {
                PlayerPrefs.DeleteKey(GetLevelKey(i));
            }
        }

        private void LoadLevelList()
        {
            for(int i = 0; i < _levelCount; i++)
            {
                string jsonLevel = PlayerPrefs.GetString(GetLevelKey(i), string.Empty);
                Assert.IsFalse(string.IsNullOrEmpty(jsonLevel));

                LevelData level = JsonUtility.FromJson<LevelData>(jsonLevel);
                _levelDatas.Add(level);
            }
        }

        private void MoveLevelsDataLeft(int startIndex)
        {
            for (int i = startIndex; i < _levelCount - 1; i++)
            {
                string nextLevel = PlayerPrefs.GetString(GetLevelKey(i + 1));
                PlayerPrefs.SetString(GetLevelKey(i), nextLevel);
            }

            PlayerPrefs.DeleteKey(GetLevelKey(_levelCount - 1));
        }

        private string GetLevelKey(int levelIndex) => PlayerPrefsKeys.LEVEL + levelIndex;

        private void UpdateLevelCount(int newCount)
        {
            _levelCount = newCount;
            PlayerPrefs.SetInt(PlayerPrefsKeys.LEVEL_COUNT, newCount);
        }
    }
}