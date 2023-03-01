using System.Collections.Generic;

namespace KSP_OrbitManeuvers.BodyConstants
{
    public class KerbolSystem
    {
        public List<string> bodies;

        public KerbolSystem()
        {
            bodies = new List<string>()
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