//namespace KSP_OrbitManeuvers.Enums
public enum CelestialBodiesEnum
{
    // 1st digit: Star
    // 2nd digit: Planet
    // 3rd digit: Moon

    #region Kerbol
    KERBOLSYSTEM = 1000,

    KERBOL = 100,

    MOHO = 110,

    EVE = 120,
    GILLY = 121,

    KERBIN = 130,
    MUN = 131,
    MINMUS = 132,

    DUNA = 140,
    IKE = 141,

    DRES = 150,

    JOOL = 160,
    LAYTHE = 161,
    VALL = 162,
    TYLO = 163,
    BOP = 164,
    POL = 165,

    EELOO = 170,
    #endregion

    #region Sol
    SOLSYSTEM = 2000,

    SOL = 200,

    MERCURY = 210,
    
    VENUS = 220,

    EARTH = 230,
    LUNA = 231,

    MARS = 240,
    PHOBOS = 241,
    DEIMOS = 242,

    JUPITER = 250,
    IO = 251,
    EUROPA = 252,
    GANYMED = 253,
    KALLISTO = 254,

    SATURN = 260,
    TETHYS = 261,
    DIONE = 262,
    RHEA = 263,
    TITAN = 264,
    IAPETUS = 265,

    URANUS = 270,
    MIRANDA = 271,
    ARIEL = 272,
    UMBRIEL = 273,
    TITANIA = 274,
    OBERON = 275,

    NEPTUN = 280,
    TRITON = 281,
    NEREID = 282,

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
