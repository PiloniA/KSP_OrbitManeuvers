using System.Collections.Generic;

namespace KSP_OrbitManeuvers.BodyConstants
{
    public class KerbolSystem
    {
        public List<string> Bodies { get; set; }
        public const double GravitationalConstant = 6.6743e-11;

        public KerbolSystem()
        {
            Bodies = new List<string>()
            {
                "Kerbol",
                "Moho",
                "Eve",
                "Gilly",
                "Kerbin",
                "Mun",
                "Minmus",
                "Duna",
                "Ike",
                "Dres",
                "Jool",
                "Laythe",
                "Vall",
                "Tylo",
                "Bop",
                "Pol",
                "Eeloo"
            };
        }
    }
}