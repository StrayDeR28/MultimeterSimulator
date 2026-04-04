using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scrpits.ScriptableObjects;

namespace Assets.Scrpits.Multimeter
{
    public class MultimeterController : MonoBehaviour
    {
        public event Action<MeasurementMode, float> MeasurementModeChanged;

        [SerializeField] private DCSource testDCSource; // for test set from inspector
        [SerializeField] private MultimeterArrow arrow;

        public bool IsPointerOnArrow { get; private set; }

        private readonly MultimeterModel _multimeterModel = new();
        private readonly int _maxMeasurementMode = Enum.GetValues(typeof(MeasurementMode)).Length - 1;
        private Dictionary<MeasurementMode, Func<float>> _measurementGetters;
        
        private void Awake()
        {
            InitializeMeasurementGetters();
            
            SetNewDCSource(testDCSource); // for test

            arrow.PointerEnterArrow += OnPointerEnterArrow;
            arrow.PointerExitArrow += OnPointerExitArrow;
        }

        private void InitializeMeasurementGetters()
        {
            _measurementGetters = new Dictionary<MeasurementMode, Func<float>>
            {
                { MeasurementMode.Neutral, () => 0f },
                { MeasurementMode.DCVoltage, () => _multimeterModel.DCVoltage },
                { MeasurementMode.ACVoltage, () => _multimeterModel.ACVoltage },
                { MeasurementMode.CurrentStrength, () => _multimeterModel.CurrentStrength },
                { MeasurementMode.Resistance, () =>_multimeterModel.Resistance },
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

            int currentIdx = (int)_multimeterModel.сurrentMeasurementMode;
            int newIdx;

            if (scroll > 0)
            {
                newIdx = currentIdx + 1;
                if (newIdx > _maxMeasurementMode) 
                {
                    newIdx = 0;
                }
            }
            else
            {
                newIdx = currentIdx - 1;
                if (newIdx < 0)
                {
                    newIdx = _maxMeasurementMode;   
                }
            }

            _multimeterModel.сurrentMeasurementMode = (MeasurementMode)newIdx;
            MeasurementModeChanged?.Invoke(_multimeterModel.сurrentMeasurementMode, GetCurrentMeasurment());
        }

        private float GetCurrentMeasurment()
        {
            return _measurementGetters[_multimeterModel.сurrentMeasurementMode]();
        }

        private void OnDestroy() 
        {
            arrow.PointerEnterArrow -= OnPointerEnterArrow;
            arrow.PointerExitArrow -= OnPointerExitArrow;
        }
    }
}