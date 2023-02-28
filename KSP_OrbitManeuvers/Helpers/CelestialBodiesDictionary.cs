using System.Collections.Generic;
using KSP_OrbitManeuvers.Enums;

namespace KSP_OrbitManeuvers.Helpers
{
    public class CelestialBodiesDictionary
    {
        readonly public Dictionary<int, string> celestialBodyCodes = new Dictionary<int, string>()
        {
            {(int)CelestialBodiesEnum.KERBOLSYSTEM , "Kerbolsystem" },

            {(int)CelestialBodiesEnum.KERBOL , "Kerbol" },

            {(int)CelestialBodiesEnum.MOHO , "Moho" },

            {(int)CelestialBodiesEnum.EVE , "Eve" },
            {(int)CelestialBodiesEnum.GILLY , "Gilly" },

            {(int)CelestialBodiesEnum.KERBIN , "Kerbin" },
            {(int)CelestialBodiesEnum.MUN , "Mun" },
            {(int)CelestialBodiesEnum.MINMUS , "Minmus" },

            {(int)CelestialBodiesEnum.DUNA , "Duna" },
            {(int)CelestialBodiesEnum.IKE , "Ike" },

            {(int)CelestialBodiesEnum.DRES , "Dres" },

            {(int)CelestialBodiesEnum.JOOL , "Jool" },
            {(int)CelestialBodiesEnum.LAYTHE , "Laythe" },
            {(int)CelestialBodiesEnum.VALL , "Vall" },
            {(int)CelestialBodiesEnum.TYLO , "Tylo" },
            {(int)CelestialBodiesEnum.BOP , "Bop" },
            {(int)CelestialBodiesEnum.POL , "Pol" },

            {(int)CelestialBodiesEnum.EELOO, "Eeloo" },


            {(int)CelestialBodiesEnum.SOLSYSTEM , "Solsystem" },

            {(int)CelestialBodiesEnum.SOL , "Sol" },

            {(int)CelestialBodiesEnum.MERCURY , "Mercury" },

            {(int)CelestialBodiesEnum.VENUS , "Venus" },

            {(int)CelestialBodiesEnum.EARTH , "Earth" },
            {(int)CelestialBodiesEnum.LUNA , "Luna" },

            {(int)CelestialBodiesEnum.MARS , "Mars" },
            {(int)CelestialBodiesEnum.PHOBOS , "Phobos" },
            {(int)CelestialBodiesEnum.DEIMOS, "Deimos" },

            {(int)CelestialBodiesEnum.JUPITER , "Jupiter" },
            {(int)CelestialBodiesEnum.IO, "Io" },
            {(int)CelestialBodiesEnum.EUROPA , "Europa" },
            {(int)CelestialBodiesEnum.GANYMED, "Ganymed" },
            {(int)CelestialBodiesEnum.KALLISTO , "Kallisto" },

            {(int)CelestialBodiesEnum.SATURN , "Saturn" },
            {(int)CelestialBodiesEnum.TETHYS, "Tethys" },
            {(int)CelestialBodiesEnum.DIONE , "Dione" },
            {(int)CelestialBodiesEnum.RHEA, "Rhea" },
            {(int)CelestialBodiesEnum.TITAN , "Titan" },
            {(int)CelestialBodiesEnum.IAPETUS, "Iapethus" },

            {(int)CelestialBodiesEnum.URANUS , "Uranus" },
            {(int)CelestialBodiesEnum.MIRANDA, "Miranda" },
            {(int)CelestialBodiesEnum.ARIEL , "Ariel" },
            {(int)CelestialBodiesEnum.UMBRIEL, "Umbriel" },
            {(int)CelestialBodiesEnum.TITANIA , "Titania" },
            {(int)CelestialBodiesEnum.OBERON, "Oberon" },

            {(int)CelestialBodiesEnum.NEPTUN , "Neptun" },
            {(int)CelestialBodiesEnum.TRITON , "Triton" },
            {(int)CelestialBodiesEnum.NEREID , "Nereid" }
        };

        public CelestialBodiesDictionary()
        {
        }
    }
}