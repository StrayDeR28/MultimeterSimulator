using TMPro;
using UnityEngine;

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
            _defaultDCVoltage = $"{MultimeterUIData.GetUiSymbol(MeasurmentMode.DCVoltage)}: 0";
            _defaultACVoltage = $"{MultimeterUIData.GetUiSymbol(MeasurmentMode.ACVoltage)}: 0";
            _defaultCurrent = $"{MultimeterUIData.GetUiSymbol(MeasurmentMode.CurrentStrength)}: 0";
            _defaultResistance = $"{MultimeterUIData.GetUiSymbol(MeasurmentMode.Resistance)}: 0";

            multimeterController.MeasurementModeChanged += MeasurementModeChanged;
        }

        private void SetDefaultMeasurements()
        {
            dCVoltage.text = _defaultDCVoltage;
            aCVoltage.text = _defaultACVoltage;
            currentStrength.text = _defaultCurrent;
            resistance.text = _defaultResistance;
        }

        private void MeasurementModeChanged(MeasurmentMode measurmentMode, float currentMeasurment)
        {
            SetDefaultMeasurements();

            string symbol = MultimeterUIData.GetUiSymbol(measurmentMode);
            if (string.IsNullOrEmpty(symbol))
            {
                return;
            }

            string displayText = $"{symbol}: {currentMeasurment.ToString("F2")}";

            switch (measurmentMode)
            {
                case MeasurmentMode.DCVoltage:
                    dCVoltage.text = displayText;
                    break;
                case MeasurmentMode.ACVoltage:
                    aCVoltage.text = displayText;
                    break;
                case MeasurmentMode.CurrentStrength:
                    currentStrength.text = displayText;
                    break;
                case MeasurmentMode.Resistance:
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