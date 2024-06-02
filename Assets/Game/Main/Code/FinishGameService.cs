using UnityEngine;

namespace Archaeologist.MainModule
{
    public sealed class FinishGameService
    {
        private const int FinishRewardsCount = 3;
        
        private readonly MainInfoModel _mainInfoModel;
        private readonly RewardsService _rewardsService;
        private readonly RestartService _restartService;

        public FinishGameService(MainInfoModel mainInfoModel, RewardsService rewardsService, RestartService restartService)
        {
            _mainInfoModel = mainInfoModel;
            _rewardsService = rewardsService;
            _restartService = restartService;
        }

        public void Init()
        {
            _mainInfoModel.OnShovelsChanged += HandleShovelsChanged;
            _mainInfoModel.OnCollectedRewardsChanged += HandleRewardsCountChanged;
        }

        public void Dispose()
        {
            _mainInfoModel.OnShovelsChanged -= HandleShovelsChanged;
            _mainInfoModel.OnCollectedRewardsChanged -= HandleRewardsCountChanged;
        }

        private void HandleShovelsChanged()
        {
            if(_mainInfoModel.ShovelCount > 0)
                return;

            //Проверка есть ли на поле награда, которую еще не собрали
            if (_rewardsService.ActiveRewardsCount >= FinishRewardsCount)
                return;
            
            _restartService.InvokeRestartGame();
            Debug.Log($"Вы проиграли! Игра перезапущена.");
        }

        private void HandleRewardsCountChanged()
        {
            if(_mainInfoModel.CollectedRewardsCount < FinishRewardsCount)
                return;

            _restartService.InvokeRestartGame();
            Debug.Log($"Поздравляю! Вы собрали нужное кол-во слитков: {FinishRewardsCount}. Игра перезапущена.");
        }
    }
}