using System.Collections.Generic;

namespace Assets.Scrpits.Multimeter
{
    public static class MultimeterUIData
    {
        public class MeasurementModeData
        {
            public string UiSymbol { get; set; }
            public float RotationAngle { get; set; }
        }

        private static readonly Dictionary<MeasurementMode, MeasurementModeData> Data = new()
        {
            {
                MeasurementMode.Neutral, new MeasurementModeData
                {
                    UiSymbol = "",
                    RotationAngle = 0f
                }
            },
            {
                MeasurementMode.DCVoltage, new MeasurementModeData
                {
                    UiSymbol = "V",
                    RotationAngle = 76f
                }
            },
            {
                MeasurementMode.ACVoltage, new MeasurementModeData
                {
                    UiSymbol = "~",
                    RotationAngle = 133f
                }
            },
            {
                MeasurementMode.CurrentStrength, new MeasurementModeData
                {
                    UiSymbol = "A",
                    RotationAngle = 150f
                }
            },
            {
                MeasurementMode.Resistance, new MeasurementModeData
                {
                    UiSymbol = "Ω",
                    RotationAngle = 284f
                }
            },
        };

        public static string GetUiSymbol(MeasurementMode mode)
        {
            return Data[mode].UiSymbol;
        }

        public static float GetRotationAngle(MeasurementMode mode)
        {
            return Data[mode].RotationAngle;
        }
    }
}
