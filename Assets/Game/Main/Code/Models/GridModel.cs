using System.Collections.Generic;

namespace Archaeologist.MainModule
{
    public sealed class GridModel
    {
        private const int DefaultCellDepth = 4;

        public IReadOnlyDictionary<int, CellModel> Cells => _cells;
        public int RowsCount { get; }
        public int ColumnsCount { get; }
        
        private readonly Dictionary<int, CellModel> _cells;
        private readonly IMainSaveData _saveData;
        
        public GridModel(int rows, int columns, IMainSaveData saveData)
        {
            _cells = new Dictionary<int, CellModel>();
            RowsCount = rows;
            ColumnsCount = columns;
            _saveData = saveData;

            var cellsCount = rows * columns;
            for (var index = 0; index < cellsCount; index++)
            {
                var cellModel = new CellModel(index, DefaultCellDepth, GetCellDepth(index));
                _cells[index] = cellModel;
            }
        }

        public void Dispose()
        {
            foreach (var (index, model) in _cells)
            {
                _saveData.Cells[index] = model.CurDepth;
            }
        }

        private int GetCellDepth(int cellIndex)
        {
            if (_saveData == null)
                return DefaultCellDepth;

            if (!_saveData.Cells.TryGetValue(cellIndex, out var depth))
                return DefaultCellDepth;

            return depth;
        }

        public void ResetGrid()
        {
            foreach (var (index, cellModel) in _cells)
            {
                cellModel.Reset();
            }
        }
    }
}