using UnityEngine;
using TMPro;

namespace Assets.Scrpits.Multimeter
{
    public class MultimeterUI : MonoBehaviour
    {
        [SerializeField] private MultimeterController multimeterController;
        [SerializeField] private TMP_Text dCVoltage;
        [SerializeField] private TMP_Text aCVoltage;
        [SerializeField] private TMP_Text currentStrength;
        [SerializeField] private TMP_Text resistance;

        private string _defaultDCVoltage;
        private string _defaultACVoltage;
        private string _defaultCurrent;
        private string _defaultResistance;

        private void Awake() 
        {
            _defaultDCVoltage = $"{MultimeterUIData.GetUiSymbol(MeasurementMode.DCVoltage)}: 0";
            _defaultACVoltage = $"{MultimeterUIData.GetUiSymbol(MeasurementMode.ACVoltage)}: 0";
            _defaultCurrent = $"{MultimeterUIData.GetUiSymbol(MeasurementMode.CurrentStrength)}: 0";
            _defaultResistance = $"{MultimeterUIData.GetUiSymbol(MeasurementMode.Resistance)}: 0";

            SetDefaultMeasurements();

            multimeterController.MeasurementModeChanged += MeasurementModeChanged;
        }

        private void SetDefaultMeasurements()
        {
            dCVoltage.text = _defaultDCVoltage;
            aCVoltage.text = _defaultACVoltage;
            currentStrength.text = _defaultCurrent;
            resistance.text = _defaultResistance;
        }

        private void MeasurementModeChanged(MeasurementMode measurementMode, float currentMeasurement)
        {
            SetDefaultMeasurements();

            string symbol = MultimeterUIData.GetUiSymbol(measurementMode);
            if (string.IsNullOrEmpty(symbol))
            {
                return;
            }

            string displayText = $"{symbol}: {currentMeasurement.ToString("F2")}";

            switch (measurementMode)
            {
                case MeasurementMode.DCVoltage:
                    dCVoltage.text = displayText;
                    break;
                case MeasurementMode.ACVoltage:
                    aCVoltage.text = displayText;
                    break;
                case MeasurementMode.CurrentStrength:
                    currentStrength.text = displayText;
                    break;
                case MeasurementMode.Resistance:
                    resistance.text = displayText;
                    break;
            }
        }
        
        private void OnDestroy() 
        {
            multimeterController.MeasurementModeChanged -= MeasurementModeChanged;
        }
    }
}