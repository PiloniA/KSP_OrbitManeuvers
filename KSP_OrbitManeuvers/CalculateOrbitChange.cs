using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSP_OrbitManeuvers.Objects;

namespace KSP_OrbitManeuvers
{
    class CalculateOrbitChange
    {
        public static double CalculateDeltaV_IncreaseApoapis(CelestialBody body, double apoapsisCurrent, double apoapsisDesired, double periapsisCurrent)
        {
            double velocityPe_Current = CalculateOrbitParameters.CalculateVelocity_At_Periapsis(body, apoapsisCurrent, periapsisCurrent);
            double velocityPe_Desired = CalculateOrbitParameters.CalculateVelocity_At_Periapsis(body, apoapsisDesired, periapsisCurrent);
            double deltaV = velocityPe_Desired - velocityPe_Current;
            return deltaV;
        }
        public static double CalculateDeltaV_IncreasePeriapsis(CelestialBody body, double apoapsisCurrent, double periapsisCurrent, double periapsisDesired)
        {
            double velocityAp_Current = CalculateOrbitParameters.CalculateVelocity_At_Periapsis(body, apoapsisCurrent, periapsisCurrent);
            double velocityAp_Desired = CalculateOrbitParameters.CalculateVelocity_At_Periapsis(body, apoapsisCurrent, periapsisDesired);
            double deltaV = velocityAp_Desired - velocityAp_Current;
            return deltaV;
        }
    }
}
