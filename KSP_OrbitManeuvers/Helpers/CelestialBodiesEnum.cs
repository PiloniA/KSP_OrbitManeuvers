namespace KSP_OrbitManeuvers.Enums
{
    public enum CelestialBodiesEnum
    {
        // 1st digit: Starsystem
        // 2nd digit: Star
        // 3rd digit: Planet
        // 4th digit: Moon

        #region Kerbol
        KERBOLSYSTEM = 1000,

        KERBOL = 1100,

        MOHO = 1110,

        EVE = 1120,
        GILLY = 1121,

        KERBIN = 1130,
        MUN = 1131,
        MINMUS = 1132,

        DUNA = 1140,
        IKE = 1141,

        DRES = 1150,

        JOOL = 1160,
        LAYTHE = 1161,
        VALL = 1162,
        TYLO = 1163,
        BOP = 1164,
        POL = 1165,

        EELOO = 1170,
        #endregion

        #region Sol
        SOLSYSTEM = 2000,

        SOL = 2200,

        MERCURY = 2210,

        VENUS = 2220,

        EARTH = 2230,
        LUNA = 2231,

        MARS = 2240,
        PHOBOS = 2241,
        DEIMOS = 2242,

        JUPITER = 2250,
        IO = 2251,
        EUROPA = 2252,
        GANYMED = 2253,
        KALLISTO = 2254,

        SATURN = 2260,
        TETHYS = 2261,
        DIONE = 2262,
        RHEA = 2263,
        TITAN = 2264,
        IAPETUS = 2265,

        URANUS = 2270,
        MIRANDA = 2271,
        ARIEL = 2272,
        UMBRIEL = 2273,
        TITANIA = 2274,
        OBERON = 2275,

        NEPTUN = 2280,
        TRITON = 2281,
        NEREID = 2282,

        #endregion
    }

    public enum BodyType
    {
        UNIVERSE = 10000,
        STARSYSTEM = 1000,
        STAR = 100,
        PLANET = 10,
        MOON = 1
    }
}

