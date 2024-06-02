using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Archaeologist.MainModule
{
    public sealed class MainStorage
    {
        private const string Key = "MainSaveData";

        public IMainSaveData SaveData => _mainSaveData;
        private MainSaveData _mainSaveData;

        public void Load()
        {
            if (_mainSaveData != null) 
                return;
                
            if (PlayerPrefs.HasKey(Key))
            {
                var value = PlayerPrefs.GetString(Key);
                _mainSaveData = JsonConvert.DeserializeObject<MainSaveData>(value);
                return;
            }

            _mainSaveData = new MainSaveData
            {
                ShovelsCount = MainInfoModel.DefaultShovelsCount,
                NotCollectedRewards = new List<int>(),
                Cells = new Dictionary<int, int>()
            };
        }
        
        public void Save()
        {
            var value = JsonConvert.SerializeObject(_mainSaveData);
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }
    }

    [Serializable]
    public sealed class MainSaveData : IMainSaveData
    {
        public int ShovelsCount { get; set; }
        public int CollectedRewardsCount { get; set; }
        public List<int> NotCollectedRewards { get; set; }
        public Dictionary<int, int> Cells { get; set; }
    }

    public interface IMainSaveData
    {
        int ShovelsCount { get; set; }
        int CollectedRewardsCount { get; set; }
        List<int> NotCollectedRewards { get; }
        Dictionary<int, int> Cells { get; }
    }
}