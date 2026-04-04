using System;
using Assets.Scrpits.ScriptableObjects;
using UnityEngine;

namespace Assets.Scrpits.Multimeter
{
    public class MultimeterController : MonoBehaviour
    {
        public event Action MeasurementModeChanged;

        [SerializeField] private DCSource testDCSource; // for test set from inspector
        [SerializeField] private MultimeterArrow arrow;

        private readonly MultimeterModel _multimeterModel = new();
        private bool _isPointerOnArrow;
        private int _maxMeasurmentMode = Enum.GetValues(typeof(MultimeterModel.MeasurmentMode)).Length - 1;
        
        private void Awake()
        {
            SetNewDCSource(testDCSource); // for test

            arrow.PointerEnterArrow += OnPointerEnterArrow;
            arrow.PointerExitArrow += OnPointerExitArrow;
        }

        public void SetNewDCSource(DCSource dCSource)
        {
            _multimeterModel.MeasureNewDCSource(dCSource.Resistance, dCSource.Power);
        }

        private void OnPointerExitArrow()
        {
            _isPointerOnArrow = false;
        }

        private void OnPointerEnterArrow()
        {
            _isPointerOnArrow = true;
        }

        private void Update() 
        {
            if (!_isPointerOnArrow) 
            {
                return;
            }

            float scroll = Input.mouseScrollDelta.y;
            if (Mathf.Approximately(scroll, 0f)) 
            {
                return;
            }

            int currentIdx = (int)_multimeterModel.сurrentMeasurmentMode;

            int newIdx;
            if (scroll > 0)
            {
                newIdx = currentIdx + 1;
                if (newIdx > _maxMeasurmentMode) newIdx = 0;
            }
            else
            {
                newIdx = currentIdx - 1;
                if (newIdx < 0) newIdx = _maxMeasurmentMode;
            }

            _multimeterModel.сurrentMeasurmentMode = (MultimeterModel.MeasurmentMode)newIdx;
            MeasurementModeChanged?.Invoke();
            Debug.Log($"MesMode upd, new mode: {_multimeterModel.сurrentMeasurmentMode}");
        }

        private void OnDestroy() 
        {
            arrow.PointerEnterArrow -= OnPointerEnterArrow;
            arrow.PointerExitArrow -= OnPointerExitArrow;
        }
    }
}