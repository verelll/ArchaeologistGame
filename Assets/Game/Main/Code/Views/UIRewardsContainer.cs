using System;
using System.Collections.Generic;
using UnityEngine;

namespace Archaeologist.MainModule
{
    public sealed class UIRewardsContainer : MonoBehaviour
    {
        [SerializeField] private Transform _rewardsContainer;
        [SerializeField] private UIRewardView _prefab;

        private readonly Dictionary<int, UIRewardView> _rewards = new Dictionary<int, UIRewardView>();
        private RewardsService _rewardsService;
        
        public void CreateView(int index, 
            Vector3 position, 
            Action<UIRewardView> startDragCallback, 
            Action<UIRewardView> dragCallback, 
            Action<UIRewardView> endDragCallback)
        {
            var createdView = Instantiate(_prefab, _rewardsContainer);
            createdView.Init(index, startDragCallback, dragCallback, endDragCallback);
            createdView.transform.position = position;
            _rewards[index] = createdView;
        }

        public void RemoveView(int index)
        {
            if(!_rewards.TryGetValue(index, out var view))
                return;
            
            Destroy(view.gameObject);
            _rewards.Remove(index);
        }
    }
}