using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSP_OrbitManeuvers.Objects
{
    class Planet : CelestialBody
    {
        public List<Moon> Moons { get; set; }

        // Orbital Characteristics
        public float SemiMajorAxis { get; set; }
        public float Apoapsis { get; set; }
        public float Periapsis { get; set; }
        public float OrbitalEccentricity { get; set; }
        public float OrbitalInclination { get; set; }
        public float ArgumentOfPeriapsis { get; set; }
        public float LongitudeOfAscendingNode { get; set; }
        public float MeanAnomalyAtBigBang { get; set; }
        public float SiderealOrbitalPeriod { get; set; }
        public float SynodicOrbitalPeriod { get; set; }

        // Physical Characteristics
        public float SolarDay { get; set; }
    }
}
