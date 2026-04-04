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

        private void Awake() 
        {
            SetDefaultMeasurements();

            multimeterController.MeasurementModeChanged += MeasurementModeChanged;
        }

        private void SetDefaultMeasurements()
        {
            dCVoltage.text = "V: 0";
            aCVoltage.text = "~: 0";
            currentStrength.text = "A: 0";
            resistance.text = "Ω: 0";
        }

        private void MeasurementModeChanged(MeasurmentMode measurmentMode, float currentMeasurment)
        {
            SetDefaultMeasurements();

            switch (measurmentMode)
            {
                case MeasurmentMode.Neutral:
                    return;
                case MeasurmentMode.DCVoltage:
                    dCVoltage.text = "V: " + currentMeasurment.ToString("F2");
                    break;
                case MeasurmentMode.ACVoltage:
                    aCVoltage.text = "~: " + currentMeasurment.ToString("F2");
                    break;
                case MeasurmentMode.CurrentStrength:
                    currentStrength.text = "A: " + currentMeasurment.ToString("F2");
                    break;
                case MeasurmentMode.Resistance:
                    resistance.text = "Ω: " + currentMeasurment.ToString("F2");
                    break;
            }
        }
        
        private void OnDestroy() 
        {
            multimeterController.MeasurementModeChanged -= MeasurementModeChanged;
        }
    }
}