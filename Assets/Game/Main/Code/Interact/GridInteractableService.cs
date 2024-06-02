using System;
using UnityEngine;

namespace Archaeologist.MainModule
{
    public sealed class GridInteractableService 
    {
        private const int LeftMouseButtonIndex = 0;

        public event Action<int, Transform> OnCellClicked;

        public void Update()
        {
            if (!Input.GetMouseButtonDown(LeftMouseButtonIndex))
                return;

            ScreenClicked(Input.mousePosition);
        }

        private void ScreenClicked(Vector2 clickScreenPos)
        {
            var ray = Camera.main.ScreenPointToRay(clickScreenPos);
            var hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider == null)
                return;
            
            var cellView = hit.collider.GetComponent<CellView>() 
                               ?? hit.collider.GetComponentInParent<CellView>();
			
            if(cellView == null)
                return;

            OnCellClicked?.Invoke(cellView.CellIndex, cellView.transform);
        }
    }
}