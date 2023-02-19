namespace KSP_OrbitManeuvers.BodyConstants
{
    public class KerbolSystem : DummySystem
    {
        public const int Kerbol_NumberOfDirectChildren = 5;

        public const float Kerbol_EquatorialRadius = 261600000F;
        public const float Kerbol_Mass = 1.7565459e28F;
        public const float Kerbol_Density = 234.23818F;
        public const float Kerbol_EscapeVelocity = 94672.01F;
        public const float Kerbol_SiderealRotationPeriod = 432000F;
        public const float Kerbol_SphereOfInfluenct = float.PositiveInfinity;

        public const bool Kerbol_AtmospherePresent = true;
        public const float Kerbol_AtmosphericPressure = 16F;
        public const float Kerbol_AtmosphericHeight = 600000;
        public const float Kerbol_TemperatureMin = 4014.95F;
        public const float Kerbol_TemperatureMax = 9621.2F;
        public const bool Kerbol_OxygenPresent = false;

        public KerbolSystem()
        {

        }
    }
}