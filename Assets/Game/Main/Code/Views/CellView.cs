using TMPro;
using UnityEngine;

namespace Archaeologist.MainModule
{
    public sealed class CellView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _heightText;

        public int CellIndex => _cellModel.CellIndex;
        
        private CellModel _cellModel;
        
        internal void SetModel(CellModel cellModel)
        {
            if (_cellModel != null)
            {
                _cellModel.OnDepthChanged -= HandleDepthChanged;
            }
            
            _cellModel = cellModel;
            
            if (_cellModel != null)
            {
                _cellModel.OnDepthChanged += HandleDepthChanged;
            }
            
            UpdateView();
        } 
        
        private void UpdateView()
        {
            if(_cellModel == null)
                return;
            
            var canDig = _cellModel.CanDig();
            _heightText.gameObject.SetActive(canDig);
            
            if(!canDig)
                return;
            
            _heightText.text = _cellModel.CurDepth.ToString();
        }

        private void HandleDepthChanged()
        {
            UpdateView();
        }
    }
}