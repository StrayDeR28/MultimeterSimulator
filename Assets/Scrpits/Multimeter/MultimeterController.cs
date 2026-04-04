using System;
using System.Collections.Generic;
using Assets.Scrpits.ScriptableObjects;
using UnityEngine;

namespace Assets.Scrpits.Multimeter
{
    public class MultimeterController : MonoBehaviour
    {
        public event Action<MeasurmentMode, float> MeasurementModeChanged;

        [SerializeField] private DCSource testDCSource; // for test set from inspector
        [SerializeField] private MultimeterArrow arrow;

        public bool IsPointerOnArrow { get; private set; }

        private readonly MultimeterModel _multimeterModel = new();
        private readonly int _maxMeasurmentMode = Enum.GetValues(typeof(MeasurmentMode)).Length - 1;
        private Dictionary<MeasurmentMode, Func<float>> _measurementGetters;
        
        private void Awake()
        {
            InitializeMeasurementGetters();
            
            SetNewDCSource(testDCSource); // for test

            arrow.PointerEnterArrow += OnPointerEnterArrow;
            arrow.PointerExitArrow += OnPointerExitArrow;
        }

        private void InitializeMeasurementGetters()
        {
            _measurementGetters = new Dictionary<MeasurmentMode, Func<float>>
            {
                { MeasurmentMode.Neutral, () => 0f },
                { MeasurmentMode.DCVoltage, () => _multimeterModel.DCVoltage },
                { MeasurmentMode.ACVoltage, () => _multimeterModel.ACVoltage },
                { MeasurmentMode.CurrentStrength, () => _multimeterModel.CurrentStrength },
                { MeasurmentMode.Resistance, () =>_multimeterModel.Resistance },
            };
        }

        public void SetNewDCSource(DCSource dCSource)
        {
            _multimeterModel.MeasureNewDCSource(dCSource.Resistance, dCSource.Power);
        }

        private void OnPointerExitArrow()
        {
            IsPointerOnArrow = false;
        }

        private void OnPointerEnterArrow()
        {
            IsPointerOnArrow = true;
        }

        private void Update() 
        {
            if (!IsPointerOnArrow) 
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
                if (newIdx > _maxMeasurmentMode) 
                {
                    newIdx = 0;
                }
            }
            else
            {
                newIdx = currentIdx - 1;
                if (newIdx < 0)
                {
                    newIdx = _maxMeasurmentMode;   
                }
            }

            _multimeterModel.сurrentMeasurmentMode = (MeasurmentMode)newIdx;
            MeasurementModeChanged?.Invoke(_multimeterModel.сurrentMeasurmentMode, GetCurrentMeasurment());
        }

        private float GetCurrentMeasurment()
        {
            return _measurementGetters[_multimeterModel.сurrentMeasurmentMode]();
        }

        private void OnDestroy() 
        {
            arrow.PointerEnterArrow -= OnPointerEnterArrow;
            arrow.PointerExitArrow -= OnPointerExitArrow;
        }
    }
}