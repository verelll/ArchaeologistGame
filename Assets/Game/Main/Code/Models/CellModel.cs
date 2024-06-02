using System;

namespace Archaeologist.MainModule
{
    public sealed class CellModel
    {
        public int CellIndex { get; }
        public int MaxDepth { get; }
        public int CurDepth { get; private set; }

        public event Action OnDepthChanged;
        
        public CellModel(int index, int maxDepth, int curDepth)
        {
            CellIndex = index;
            MaxDepth = maxDepth;
            CurDepth = curDepth;
        }

        public bool CanDig()
        {
            return CurDepth > 0;
        }

        public void Dig()
        {
            if (!CanDig())
                return;

            CurDepth--;
            OnDepthChanged?.Invoke();
        }

        public void Reset()
        {
            CurDepth = MaxDepth;
            OnDepthChanged?.Invoke();
        }
    }
}