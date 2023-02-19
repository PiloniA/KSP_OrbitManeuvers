namespace KSP_OrbitManeuvers.BodyConstants
{
    public class SolarSystem : DummySystem
    {
        public const int Sol_NumberOfDirectChildren = 5;

        public const float Sol_EquatorialRadius = 261600000F;
        public const float Sol_Mass = 1.7565459e28F;
        public const float Sol_Density = 234.23818F;
        public const float Sol_EscapeVelocity = 94672.01F;
        public const float Sol_SiderealRotationPeriod = 432000F;
        public const float Sol_SphereOfInfluenct = float.PositiveInfinity;

        public const bool Sol_AtmospherePresent = true;
        public const float Sol_AtmosphericPressure = 16F;
        public const float Sol_AtmosphericHeight = 600000;
        public const float Sol_TemperatureMin = 4014.95F;
        public const float Sol_TemperatureMax = 9621.2F;
        public const bool Sol_OxygenPresent = false;

        public SolarSystem()
        {

        }
    }
}