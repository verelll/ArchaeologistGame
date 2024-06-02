using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Archaeologist.MainModule
{
    public sealed class UIRewardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public int CellIndex { get; private set; }

        private Action<UIRewardView> _startDragCallback;
        private Action<UIRewardView> _dragCallback;
        private Action<UIRewardView> _endDragCallback;

        public void Init(
            int index, 
            Action<UIRewardView> startDragCallback, 
            Action<UIRewardView> dragCallback, 
            Action<UIRewardView> endDragCallback)
        {
            _startDragCallback = startDragCallback;
            _dragCallback = dragCallback;
            _endDragCallback = endDragCallback;
            CellIndex = index;
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _startDragCallback?.Invoke(this);
        }
        
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _dragCallback?.Invoke(this);
        }
        
        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _endDragCallback?.Invoke(this);
        }
    }
}