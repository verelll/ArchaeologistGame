using System;
using UnityEngine;
using UnityEngine.UI;

namespace Archaeologist.MainModule
{
    public sealed class UIRestartView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

        private Action _restartCallback;
        
        public void Init(Action restartCallback)
        {
            _restartCallback = restartCallback;
            _restartButton.onClick.AddListener(HandleButtonClicked);
        }
        
        public void Dispose()
        {
            _restartButton.onClick.RemoveListener(HandleButtonClicked);
        }

        private void HandleButtonClicked()
        {
            _restartCallback?.Invoke();
        }
    }
}