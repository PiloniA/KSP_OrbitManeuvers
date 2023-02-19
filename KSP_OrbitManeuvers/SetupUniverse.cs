using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KSP_OrbitManeuvers.Objects;
using KSP_OrbitManeuvers.Helpers;
using KSP_OrbitManeuvers.BodyConstants;
using System.Reflection;

namespace KSP_OrbitManeuvers
{
    public class SetupUniverse
    {
        DummySystem starSystem = new DummySystem();
        readonly CelestialBodiesDictionary celestialDict = new CelestialBodiesDictionary();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            var pr = new SetupUniverse();

            List<string> starSystemNames = new List<string>
            {
                "Kerbol System"//,
                //"Solar System"
            };

            Universe universe = pr.CreateUniverse(starSystemNames);

            pr.PrintUniverse(universe);

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }

        private Universe CreateUniverse(List<string> starSystemNames)
        {
            Universe universe = new Universe();
            foreach(string starSystemName in starSystemNames)
            {
                StarSystem starsystem = CreateStarSystem(starSystemName);
                universe.Starsystems.Add(starsystem);
            }
            return universe;
        }

        private StarSystem CreateStarSystem(string starSystemName)
        {
            int starNumber = -1;
            if (starSystemName == "Kerbol System")
            {
                starNumber = (int)CelestialBodiesEnum.KERBOL;
            }
            else if (starSystemName == "Solar System")
            {
                starNumber = (int)CelestialBodiesEnum.SOL;
            }
            else
            {
                TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine("Star System not defined. Aborting...");
                System.Windows.Forms.Application.Exit();
            }

            StarSystem starSystem = new StarSystem();
            starSystem.SystemName = starSystemName;
                
            Star star = CreateStar(starNumber);
            starSystem.Stars.Add(star);
            return starSystem;

        }

        private Star CreateStar(int starNumber)
        {
            string starName = celestialDict.celestialBodyCodes[starNumber];

            if (starName == "Kerbol")
            {
                starSystem = new KerbolSystem();
            }
            else if (starName == "Sol")
            {
                starSystem = new SolarSystem();
            }
            else
            {
                TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine("Star System not defined. Abort...");
                System.Windows.Forms.Application.Exit();
            }

            Star star = new Star()
            {
                Name = starName,

                NumberOfDirectChildren = Convert.ToInt32(starSystem.GetType().GetField($"{starName}_NumberOfDirectChildren").GetValue(starSystem)),
                EquatorialRadius = (float)starSystem.GetType().GetField($"{starName}_EquatorialRadius").GetValue(starSystem),
                Mass = (float)starSystem.GetType().GetField($"{starName}_Mass").GetValue(starSystem),
                Density = (float)starSystem.GetType().GetField($"{starName}_Density").GetValue(starSystem),
                EscapeVelocity = (float)starSystem.GetType().GetField($"{starName}_EscapeVelocity").GetValue(starSystem),
                SiderealRotationPeriod = (float)starSystem.GetType().GetField($"{starName}_SiderealRotationPeriod").GetValue(starSystem),
                SphereOfInfluence = (float)starSystem.GetType().GetField($"{starName}_SphereOfInfluence").GetValue(starSystem),

                AtmospherePresent = Convert.ToBoolean(starSystem.GetType().GetField($"{starName}_AtmospherePresent").GetValue(starSystem)),
                AtmosphericPressure = (float)starSystem.GetType().GetField($"{starName}_AtmosphericPressure").GetValue(starSystem),
                AtmosphericHeight = (float)starSystem.GetType().GetField($"{starName}_AtmosphericHeight").GetValue(starSystem),
                TemperatureMin = (float)starSystem.GetType().GetField($"{starName}_TemperatureMin").GetValue(starSystem),
                TemperatureMax = (float)starSystem.GetType().GetField($"{starName}_TemperatureMax").GetValue(starSystem),
                OxigenPresent = Convert.ToBoolean(starSystem.GetType().GetField($"{starName}_OxygenPresent").GetValue(starSystem))
            };

            for (int i = 1; i <= star.NumberOfDirectChildren; i++)
            {
                int planetNumber = starNumber + 10 * i;
                Planet planet = CreatePlanet(planetNumber);
                star.Planets.Add(planet);
            }
            
            return star;

        }

        private Planet CreatePlanet(int planetNumber)
        {
            string planetName = celestialDict.celestialBodyCodes[planetNumber];

            Planet planet = new Planet
            {
                Name = planetName,

                NumberOfDirectChildren = Convert.ToInt32(starSystem.GetType().GetField($"{planetName}_NumberOfDirectChildren").GetValue(starSystem)),
                EquatorialRadius = (float)starSystem.GetType().GetField($"{planetName}_EquatorialRadius").GetValue(starSystem),
                Mass = (float)starSystem.GetType().GetField($"{planetName}_Mass").GetValue(starSystem),
                Density = (float)starSystem.GetType().GetField($"{planetName}_Density").GetValue(starSystem),
                EscapeVelocity = (float)starSystem.GetType().GetField($"{planetName}_EscapeVelocity").GetValue(starSystem),
                SiderealRotationPeriod = (float)starSystem.GetType().GetField($"{planetName}_SiderealRotationPeriod").GetValue(starSystem),
                SphereOfInfluence = (float)starSystem.GetType().GetField($"{planetName}_SphereOfInfluence").GetValue(starSystem),

                AtmospherePresent = Convert.ToBoolean(starSystem.GetType().GetField($"{planetName}_AtmospherePresent").GetValue(starSystem)),
                AtmosphericPressure = (float)starSystem.GetType().GetField($"{planetName}_AtmosphericPressure").GetValue(starSystem),
                AtmosphericHeight = (float)starSystem.GetType().GetField($"{planetName}_AtmosphericHeight").GetValue(starSystem),
                TemperatureMin = (float)starSystem.GetType().GetField($"{planetName}_TemperatureMin").GetValue(starSystem),
                TemperatureMax = (float)starSystem.GetType().GetField($"{planetName}_TemperatureMax").GetValue(starSystem),
                OxigenPresent = Convert.ToBoolean(starSystem.GetType().GetField($"{planetName}_OxygenPresent").GetValue(starSystem))
            };
            return planet;

            //CreateMoon();
        }

        private void CreateMoon()
        {

        }

        private void PrintUniverse(Universe universe)
        {
            TextWriter outWriter = Console.Out;

            foreach (StarSystem starSystem in universe.Starsystems)
            {
                outWriter.WriteLine($"StarSystem Name: {starSystem.SystemName}");
                foreach (Star star in starSystem.Stars)
                {
                    outWriter.WriteLine($"  Star Name: {star.Name}");
                    foreach (Planet planet in star.Planets)
                    {
                        outWriter.WriteLine($"    Planet Name: {planet.Name}");
                    }
                }
            }
        }
    }
}
