using System;

namespace Assets.Scrpits.Utils
{
    public static class PhysicsEquations
    {
        public static float CalculatePowerViaVoltageAndCurrent(float voltage, float currentStrength)
        {
            return voltage * currentStrength;
        }

        public static float CalculatePowerViaResistanceAndCurrent(float resistance, float currentStrength)
        {
            return resistance * currentStrength * currentStrength;
        }

        public static float CalculatePowerViaVoltageAndResistance(float voltage, float resistance)
        {
            return voltage * voltage / resistance;
        }

        public static float CalculateCurrentStrength(float resistance, float power)
        {
            return (float)Math.Sqrt(power / resistance);
        }
        
        public static float CalculateDCVoltage(float power, float currentStrength)
        {
            return power / currentStrength;
        }
    }
}