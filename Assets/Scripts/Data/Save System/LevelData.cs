using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveTogether.World;

namespace SurviveTogether.Data
{
    [System.Serializable]
    public class LevelData
    {
        public int index;
        public string name;
        public ConvertableJsonDateTime creationDate;
        public ConvertableJsonDateTime modifyDate;
        public MapConstructor mapData;
        public PlayerDataContainer playerDatas;
    }
}