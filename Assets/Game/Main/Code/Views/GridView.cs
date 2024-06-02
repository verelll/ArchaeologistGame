using System.Collections.Generic;
using UnityEngine;

namespace Archaeologist.MainModule
{
    public sealed class GridView : MonoBehaviour
    {
        [SerializeField] private float _spacing;
        [SerializeField] private CellView _cellPrefab;
        [SerializeField] private Transform _container;
        
        private Dictionary<int, CellView> _cellsView;
        private GridModel _gridModel;

        internal void SetModel(GridModel gridModel)
        {
            ClearGrid();
            _gridModel = gridModel;
            GenerateGrid();
        }

        public Vector3 GetCellPosByIndex(int index)
        {
            if (!_cellsView.TryGetValue(index, out var cellView))
                return default;

            return cellView.transform.position;
        }
        
        private void GenerateGrid()
        {
            _cellsView = new Dictionary<int, CellView>();
            var columns = _gridModel.ColumnsCount;
            var rows = _gridModel.RowsCount;
            
            var xOffset = (columns - 1) * _spacing / 2;
            var yOffset = (rows - 1) * _spacing / 2;
            
            var index = 0;
            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    if (!_gridModel.Cells.TryGetValue(index, out var cellModel))
                        continue;
                    
                    var cellPos = transform.position + new Vector3(column * _spacing - xOffset, row * _spacing - yOffset, 0);
                    var createdCell = Instantiate(_cellPrefab, cellPos, Quaternion.identity, _container);
                    createdCell.SetModel(cellModel);
                    _cellsView[index] = createdCell;
                    index++;
                }
            }
        }

        private void ClearGrid()
        {
            if(_gridModel == null 
               || _cellsView == null 
               || _cellsView.Count == 0)
                return;
            
            foreach (var (index, cellView) in _cellsView)
            {
                Destroy(cellView.gameObject);
            }
            
            _cellsView.Clear();
        }
    }
}