using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Archaeologist.MainModule
{
    public sealed class UIRewardBagView : MonoBehaviour, IDropHandler
    {
        private Action _dropCallback;
        
        public void Init(Action dropCallback)
        {
            _dropCallback = dropCallback;
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            _dropCallback?.Invoke();
        }
    }
}