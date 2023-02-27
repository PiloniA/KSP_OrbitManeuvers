using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public float EquatorialRadius { get; set; }
        public float Density { get; set; }
        public float Mass { get; set; }
        public float EscapeVelocity { get; set; }
        public float SiderealRotationPeriod { get; set; }
        public float SolarDay { get; set; }
        public float SphereOfInfluence { get; set; }

        //Atomspheric Characteristics
        public bool AtmospherePresent { get; set; }
        public float AtmosphericPressure { get; set; }
        public float AtmosphericHeight { get; set; }
        public float TemperatureMin { get; set; }
        public float TemperatureMax { get; set; }
        public bool OxigenPresent { get; set; }

        // Orbital Characteristics
        public long SemiMajorAxis { get; set; }
        public float Apoapsis { get; set; }
        public float Periapsis { get; set; }
        public float OrbitalEccentricity { get; set; }
        public float OrbitalInclination { get; set; }
        public float ArgumentOfPeriapsis { get; set; }
        public float LongitudeOfAscendingNode { get; set; }
        public float MeanAnomalyAtBigBang { get; set; }
        public float SiderealOrbitalPeriod { get; set; }
        public float SynodicOrbitalPeriod { get; set; }

        public CelestialBody()
        {
            
        }
    }
}
