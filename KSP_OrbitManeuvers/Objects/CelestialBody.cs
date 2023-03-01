using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSP_OrbitManeuvers.Enums;

namespace KSP_OrbitManeuvers.Objects
{
    public class CelestialBody
    {
        //common Characteristics
        public BodyType Type { get; set; }
        public string Name { get; set; }
        public int NumberOfDirectChildren { get; set; }
        public List<CelestialBody> Children { get; set; }

        //Physical Characteristics
        public double? EquatorialRadius { get; set; }
        public double? Density { get; set; }
        public double? Mass { get; set; }
        public double? EscapeVelocity { get; set; }
        public double? SiderealRotationPeriod { get; set; }
        public double? SolarDay { get; set; }
        public double? SphereOfInfluence { get; set; }

        // Atomspheric Characteristics
        public bool? AtmospherePresent { get; set; }
        public double? AtmosphericPressure { get; set; }
        public double? AtmosphericHeight { get; set; }
        public double? TemperatureMin { get; set; }
        public double? TemperatureMax { get; set; }
        public bool? OxigenPresent { get; set; }

        // Orbital Characteristics
        public double? SemiMajorAxis { get; set; }
        public double? Apoapsis { get; set; }
        public double? Periapsis { get; set; }
        public double? OrbitalEccentricity { get; set; }
        public double? OrbitalInclination { get; set; }
        public double? ArgumentOfPeriapsis { get; set; }
        public double? LongitudeOfAscendingNode { get; set; }
        public double? MeanAnomalyAtBigBang { get; set; }
        public double? SiderealOrbitalPeriod { get; set; }
        public double? SynodicOrbitalPeriod { get; set; }

        public CelestialBody()
        {
            Children = new List<CelestialBody>();
        }
    }
}
