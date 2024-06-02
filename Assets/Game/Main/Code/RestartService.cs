namespace Archaeologist.MainModule
{
    public sealed class RestartService
    {
        private readonly MainInfoModel _mainInfoModel;
        private readonly GridModel _gridModel;
        private readonly RewardsService _rewardsService;
        
        private readonly UIRestartView _uiRestartView;

        public RestartService(
            MainInfoModel infoModel,
            MainService mainService, 
            RewardsService rewardsService, 
            UIRestartView uiRestartView)
        {
            _mainInfoModel = infoModel;
            _gridModel = mainService.Model;
            _rewardsService = rewardsService;
            _uiRestartView = uiRestartView;
        }

        public void Init()
        {
            _uiRestartView.Init(InvokeRestartGame);
        }
        
        public void Dispose()
        {
            _uiRestartView.Dispose();
        }
        
        public void InvokeRestartGame()
        {
            _gridModel.ResetGrid();
            _rewardsService.Reset();
            _mainInfoModel.ResetShovels();
            _mainInfoModel.ResetRewardsCount();
        }
    }
}