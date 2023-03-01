using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSP_OrbitManeuvers.Objects;
using KSP_OrbitManeuvers.BodyConstants;

namespace KSP_OrbitManeuvers
{
    class CalculateOrbitParameters
    {
        public static double CalculateSemiMajorAxis(CelestialBody body, double apoapsis, double periapss)
        {
            return (double)(apoapsis + periapss + 2 * body.EquatorialRadius)/2;
        }
        public static double CalculateSemiMinorAxis(CelestialBody body, double apoapsis, double periapss)
        {
            return (double)Math.Sqrt((apoapsis + (double)body.EquatorialRadius) * (periapss + (double)body.EquatorialRadius));
        }
        public static double CalculateEccentricity(double semiMajorAxis, double semiMinorAxis)
        {
            return Math.Sqrt(1 - Math.Pow(semiMinorAxis, 2) / Math.Pow(semiMajorAxis, 2));
        }
        public static double CalculatePeriod(CelestialBody body, double apoapsis, double periapsis)
        {
            double semiMajoraxis = CalculateSemiMajorAxis(body, apoapsis, periapsis);
            return 2*Math.PI * Math.Sqrt(Math.Pow(semiMajoraxis, 3)/(KerbolSystem.GravitationalConstant*(double)body.Mass));
        }
        public static double CalculateVelocity_At_Apoapsis(CelestialBody body, double apoapsis, double periapsis)
        {
            return CalculateVelocity_At_AnyPoint(body, apoapsis, periapsis, apoapsis);
        }
        public static double CalculateVelocity_At_Periapsis(CelestialBody body, double apoapsis, double periapsis)
        {
            return CalculateVelocity_At_AnyPoint(body, apoapsis, periapsis, periapsis);
        }
        public static double CalculateVelocity_At_AnyPoint(CelestialBody body, double apoapsis, double periapsis, double distanceAMSL)
        {
            double distance = distanceAMSL + (double)body.EquatorialRadius;
            double semiMajorAxis = CalculateSemiMajorAxis(body, apoapsis, periapsis);
            double velocity = Math.Sqrt((double)(KerbolSystem.GravitationalConstant * body.Mass * (2/distance - 1/semiMajorAxis)));
            return velocity;
        }
    }
}
