using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSP_OrbitManeuvers.Objects;

namespace KSP_OrbitManeuvers
{
    class CalculateTransfers
    {
        public static double CalculateDeltaV_InterceptChild(CelestialBody body, CelestialBody child, double apoapsisCurrent, double periapsisCurrent)
        {
            return 0;
        }

        public static double CalculateDeltaV_InterceptSibling(CelestialBody body, CelestialBody sibling, double apoapsisCurrent, double periapsisCurrent)
        {
            return 0;
        }

        public static double CalculateDeltaV_LeaveSOI(CelestialBody body, double apoapsisCurrent, double periapsisCurrent)
        {
            double soiRadius = (double)body.SphereOfInfluence;
            return 0;
        }
    }
}
