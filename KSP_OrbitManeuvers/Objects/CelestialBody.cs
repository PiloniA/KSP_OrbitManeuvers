using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSP_OrbitManeuvers.Objects
{
    class CelestialBody
    {
        //Physical Characteristics
        public float EquatorialRadius { get; set; }
        public float Density { get; set; }
        public float Mass { get; set; }
        public float EscapeVelocity { get; set; }
        public float SiderealRotationPeriod { get; set; }
        public float SphereOfInfluence { get; set; }

        //Atomspheric Characteristics
        public bool AtmospherePresent { get; set; }
        public float AtmosphericPressure { get; set; }
        public float AtmosphericHeight { get; set; }
        public float TemperatureMin { get; set; }
        public float TemperatureMax { get; set; }
        public bool OxigenPresent { get; set; }

        public CelestialBody()
        {
            
        }
    }
}
