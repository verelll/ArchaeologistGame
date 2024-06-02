using TMPro;
using UnityEngine;

namespace Archaeologist.MainModule
{
    public sealed class MainInfoView : MonoBehaviour
    {
        private const string ShovelsTerm = "Лопатки: ";
        private const string CollectedRewardsTerm = "Собрано наград: ";
        
        [SerializeField] private TMP_Text _shovelCountText;
        [SerializeField] private TMP_Text _collectedRewardsCountText;

        private MainInfoModel _model;
        
        public void SetModel(MainInfoModel model)
        {
            if (_model != null)
            {
                _model.OnShovelsChanged -= HandleShovelChanged;
                _model.OnCollectedRewardsChanged -= HandleCollectedRewardsChanged;
            }
            
            _model = model;
            
            if (_model != null)
            {
                _model.OnShovelsChanged += HandleShovelChanged;
                _model.OnCollectedRewardsChanged += HandleCollectedRewardsChanged;
            }
            
            HandleShovelChanged();
            HandleCollectedRewardsChanged();
        }

        private void HandleShovelChanged()
        {
            _shovelCountText.text = $"{ShovelsTerm}{_model.ShovelCount.ToString()}";
        }
        
        private void HandleCollectedRewardsChanged()
        {
            _collectedRewardsCountText.text =  $"{CollectedRewardsTerm}{_model.CollectedRewardsCount.ToString()}";
        }
    }
}