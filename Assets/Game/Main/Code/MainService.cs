using UnityEngine;

namespace Archaeologist.MainModule
{
    public sealed class MainService
    {
        private const int GridRowsCount = 10;
        private const int GridColumnsCount = 10;

        private readonly IMainSaveData _saveData;
        private readonly GridInteractableService _gridInteractableService;
        private readonly RewardsService _rewardsService;
        private readonly MainInfoModel _mainInfoModel;
        private readonly GridView _gridView;
        private readonly MainInfoView _infoView;

        public GridModel Model { get; }

        public MainService(
            IMainSaveData saveData, 
            GridInteractableService interactableService, 
            RewardsService rewardsService, 
            MainInfoModel mainInfoModel,
            GridView gridView, 
            MainInfoView infoView)
        {
            _saveData = saveData;
            _gridInteractableService = interactableService;
            _rewardsService = rewardsService;
            _mainInfoModel = mainInfoModel;
            _gridView = gridView;
            _infoView = infoView;
            
            Model = new GridModel(GridRowsCount, GridColumnsCount, _saveData);
        }

        public void Init()
        {
            _gridView.SetModel(Model);
            _infoView.SetModel(_mainInfoModel);
            _gridInteractableService.OnCellClicked += HandleCellClicked;
        }
        
        public void Dispose()
        {
            Model.Dispose();
            _gridInteractableService.OnCellClicked -= HandleCellClicked;
        }

        private void HandleCellClicked(int cellIndex, Transform cellTransform)
        {
            if(!Model.Cells.TryGetValue(cellIndex, out var cellModel))
                return;

            if (_rewardsService.HasRewardOnCell(cellIndex))
                return;

            var canDig = cellModel.CanDig() && _mainInfoModel.CanRemoveShovel();
            if(!canDig)
                return;

            cellModel.Dig();
            _mainInfoModel.RemoveShovel();

            _rewardsService.TryCreateRewardOnCell(cellIndex, cellTransform.position);
        }
    }
}