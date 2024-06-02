using System;
using System.Collections.Generic;
using UnityEngine;

namespace Archaeologist.MainModule
{
    public sealed class RewardsViewController
    {
        private readonly UIRewardsContainer _uiRewardsContainer;
        private readonly GridView _gridView;

        private Vector3 _startDragPos;
        private int? _draggedCellIndex;

        public event Action<int> OnDropToBag;
        
        public RewardsViewController(
            GridView gridView,
            UIRewardsContainer rewardsContainer, 
            UIRewardBagView bagView)
        {
            _gridView = gridView;
            _uiRewardsContainer = rewardsContainer;
            bagView.Init(HandleDropToBag);
        }

        public void CreateLoadedViews(List<int> cellsWithReward)
        {
            foreach (var cellIndex in cellsWithReward)
            {
                CreateRewardView(cellIndex, _gridView.GetCellPosByIndex(cellIndex));
            }
        }
        
        public void CreateRewardView(int cellIndex, Vector3 cellWorldPos)
        {
            _uiRewardsContainer.CreateView(
                cellIndex, 
                cellWorldPos, 
                view => HandleStartDragReward(view.transform, view.CellIndex), 
                view => HandleDragReward(view.transform), 
                view => HandleEndDragReward(view.transform));
        }

        public void RemoveRewardView(int cellIndex)
        {
            _uiRewardsContainer.RemoveView(cellIndex);
        }
        
        private void HandleStartDragReward(Transform viewTransform, int index)
        {
            _startDragPos = viewTransform.transform.position;
            _draggedCellIndex = index;
        }
        
        private void HandleDragReward(Transform viewTransform)
        {
            var mousePos = Input.mousePosition;
            var slotWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            viewTransform.transform.position = slotWorldPos;
        }
        
        private void HandleEndDragReward(Transform rewardView)
        {
            if(!_draggedCellIndex.HasValue)
                return;

            rewardView.transform.position = _startDragPos;
        }
        
        private void HandleDropToBag()
        {
            if(!_draggedCellIndex.HasValue)
                return;

            RemoveRewardView(_draggedCellIndex.Value);
            OnDropToBag?.Invoke(_draggedCellIndex.Value);
            _draggedCellIndex = default;
        }
    }
}