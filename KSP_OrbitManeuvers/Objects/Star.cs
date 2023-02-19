using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSP_OrbitManeuvers.Objects
{
    class Star : CelestialBody
    {
        public List<Planet> Planets { get; set; }
        public Star()
        {
            Planets = new List<Planet>();
        }
    }
}
