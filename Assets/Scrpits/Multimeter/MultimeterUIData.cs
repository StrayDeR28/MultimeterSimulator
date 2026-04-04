using System.Collections.Generic;

namespace Assets.Scrpits.Multimeter
{
    public static class MultimeterUIData
    {
        public class MeasurmentModeData
        {
            public string UiSymbol { get; set; }
            public float RotationAngle { get; set; }
        }

        private static readonly Dictionary<MeasurmentMode, MeasurmentModeData> Data = new()
        {
            {
                MeasurmentMode.Neutral, new MeasurmentModeData
                {
                    UiSymbol = "",
                    RotationAngle = 0f
                }
            },
            {
                MeasurmentMode.DCVoltage, new MeasurmentModeData
                {
                    UiSymbol = "V",
                    RotationAngle = 76f
                }
            },
            {
                MeasurmentMode.ACVoltage, new MeasurmentModeData
                {
                    UiSymbol = "~",
                    RotationAngle = 133f
                }
            },
            {
                MeasurmentMode.CurrentStrength, new MeasurmentModeData
                {
                    UiSymbol = "A",
                    RotationAngle = 150f
                }
            },
            {
                MeasurmentMode.Resistance, new MeasurmentModeData
                {
                    UiSymbol = "Ω",
                    RotationAngle = 284f
                }
            },
        };

        public static string GetUiSymbol(MeasurmentMode mode)
        {
            return Data[mode].UiSymbol;
        }

        public static float GetRotationAngle(MeasurmentMode mode)
        {
            return Data[mode].RotationAngle;
        }
    }
}
