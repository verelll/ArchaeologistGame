using UnityEngine;

namespace Archaeologist.MainModule
{
    public sealed class RewardsService
    {
        private const float RewardChance = 0.2f; //from 0 to 1

        private readonly RewardsViewController _rewardsViewController;
        private readonly MainInfoModel _mainInfoModel;
        private readonly IMainSaveData _saveData;
        
        public int ActiveRewardsCount => _saveData.NotCollectedRewards.Count;

        public RewardsService(
            RewardsViewController rewardsViewController,
            MainInfoModel mainInfoModel, 
            IMainSaveData saveData)
        {
            _rewardsViewController = rewardsViewController;
            _mainInfoModel = mainInfoModel;
            _saveData = saveData;
        }

        public void Init()
        {
            _rewardsViewController.OnDropToBag += HandleDropToBag;
            _rewardsViewController.CreateLoadedViews(_saveData.NotCollectedRewards);
        }

        public void Dispose()
        {
            _rewardsViewController.OnDropToBag -= HandleDropToBag;
        }

        public bool HasRewardOnCell(int cellIndex)
        {
            return _saveData.NotCollectedRewards.Contains(cellIndex);
        }

        public bool TryCreateRewardOnCell(int cellIndex, Vector3 cellWorldPos)
        {
            if (HasRewardOnCell(cellIndex))
                return false;
            
            var rewardChance = Random.Range(0, 1f);
            if (rewardChance >= RewardChance)
                return false;
            
            _rewardsViewController.CreateRewardView(cellIndex, cellWorldPos);
            _saveData.NotCollectedRewards.Add(cellIndex);
            return true;
        }

        public void Reset()
        {
            foreach (var index in _saveData.NotCollectedRewards)
            {
                _rewardsViewController.RemoveRewardView(index);
            }
            
            _saveData.NotCollectedRewards.Clear();
        }
        
        private void HandleDropToBag(int cellIndex)
        {
            _mainInfoModel.CollectReward();
            _saveData.NotCollectedRewards.Remove(cellIndex);
        }
    }
}