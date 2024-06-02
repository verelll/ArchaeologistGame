using System;

namespace Archaeologist.MainModule
{
    public sealed class MainInfoModel
    {
        public static int DefaultShovelsCount => 20;

        private readonly IMainSaveData _saveData;
        
        public int ShovelCount { get; private set; }
        public int CollectedRewardsCount  { get; private set; }

        public event Action OnShovelsChanged;
        public event Action OnCollectedRewardsChanged;
        
        public MainInfoModel(IMainSaveData saveData)
        {
            _saveData = saveData;
            ShovelCount = _saveData.ShovelsCount;
            CollectedRewardsCount = _saveData.CollectedRewardsCount;
        }

        public bool CanRemoveShovel()
        {
            return ShovelCount != 0;
        }
        
        public void RemoveShovel()
        {
            if (!CanRemoveShovel())
                return;

            ShovelCount--;
            _saveData.ShovelsCount = ShovelCount;
            OnShovelsChanged?.Invoke();
        }

        public void ResetShovels()
        {
            ShovelCount = DefaultShovelsCount;
            _saveData.ShovelsCount = ShovelCount;
            OnShovelsChanged?.Invoke();
        }

        public void CollectReward()
        {
            CollectedRewardsCount++;
            _saveData.CollectedRewardsCount = CollectedRewardsCount;
            OnCollectedRewardsChanged?.Invoke();
        }
        
        public void ResetRewardsCount()
        {
            CollectedRewardsCount = 0;
            _saveData.CollectedRewardsCount = CollectedRewardsCount;
            OnCollectedRewardsChanged?.Invoke();
        }
    }
}