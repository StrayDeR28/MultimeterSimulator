using Assets.Scrpits.Utils;

namespace Assets.Scrpits.Multimeter
{
    public class MultimeterModel
    {
        public enum MeasurmentMode
        {
            Neutral,
            DCVoltage,
            ACVoltage,
            CurrentStrength,
            Resistance
        }

        public MeasurmentMode сurrentMeasurmentMode = MeasurmentMode.Neutral;

        public float DCVoltage { get; private set; } = 0f;
        public float ACVoltage { get; private set; } = 0.01f;
        public float Power { get; private set; } = 0f;
        public float CurrentStrength { get; private set; } = 0f;
        public float Resistance { get; private set; } = 0f;

        public void MeasureNewDCSource(float resistance, float power)
        {
            Resistance = resistance;
            Power = power;
            ACVoltage = 0.01f;
            CurrentStrength = PhysicsEquations.CalculateCurrentStrength(resistance, power);
            DCVoltage = PhysicsEquations.CalculateDCVoltage(power, CurrentStrength);
        }
    }
}
