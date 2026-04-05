using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scrpits.ScriptableObjects;

namespace Assets.Scrpits.Multimeter
{
    public class MultimeterController : MonoBehaviour
    {
        public event Action<MeasurementMode, float> MeasurementModeChanged;

        [SerializeField] private DCSource testDCSource; // for test task
        [SerializeField] private MultimeterModeSwitcher modeSwitcher;

        private readonly MultimeterModel _multimeterModel = new();
        private static readonly int _maxMeasurementMode = Enum.GetValues(typeof(MeasurementMode)).Length - 1;
        private Dictionary<MeasurementMode, Func<float>> _measurementGetters;
        
        private void Awake()
        {
            InitializeMeasurementGetters();
            SetNewDCSource(testDCSource); // for test task
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
            _multimeterModel.MeasureNewDCSource(dCSource.resistance, dCSource.power);
        }

        private void Update() 
        {
            if (modeSwitcher.IsPointerOnModeSwitcher) 
            {
                HandleScrollInput();
            }
        }

        private void HandleScrollInput()
        {
            float scroll = Input.mouseScrollDelta.y;
            if (Mathf.Approximately(scroll, 0f))
            {
                return;
            }

            int direction = scroll > 0 ? 1 : -1;
            ChangeMeasurementMode(direction);
        }

        private void ChangeMeasurementMode(int direction)
        {
            int currentIdx = (int)_multimeterModel.CurrentMeasurementMode;
            int newIdx = currentIdx + direction;

            if (newIdx > _maxMeasurementMode)
            {
                newIdx = 0;
            }
            if (newIdx < 0)
            {
                newIdx = _maxMeasurementMode;
            }
            
            _multimeterModel.CurrentMeasurementMode = (MeasurementMode)newIdx;
            MeasurementModeChanged?.Invoke(_multimeterModel.CurrentMeasurementMode, GetCurrentMeasurementValue());
        }

        private float GetCurrentMeasurementValue()
        {
            return _measurementGetters[_multimeterModel.CurrentMeasurementMode]();
        }
    }
}