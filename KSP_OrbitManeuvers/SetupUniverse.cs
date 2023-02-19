using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KSP_OrbitManeuvers.Objects;
using KSP_OrbitManeuvers.BodyConstants;
using System.Reflection;

namespace KSP_OrbitManeuvers
{
    public class SetupUniverse
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            var pr = new SetupUniverse();

            List<string> starSystemNames = new List<string>
            {
                "Kerbol System",
                "Solar System"
            };

            pr.CreateUniverse(starSystemNames);

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }

        private void CreateUniverse(List<string> starSystemNames)
        {
            foreach(string starSystemName in starSystemNames)
            {
                CreateStarSystem(starSystemName);
            }   
        }

        private void CreateStarSystem(string starSystemName)
        {
            int starNumber;
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
                return;
            }
                
            CreateStar(starNumber);
        }

        private void CreateStar(int starNumber)
        {
            string starName;
            var starSystem = new DummySystem();

            if (starNumber == 100)
            {
                starName = "Kerbol";
                starSystem = new KerbolSystem();
            }
            else if (starNumber == 200)
            {
                starName = "Sol";
                starSystem = new SolarSystem();
            }
            else
            {
                TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine("Star System not defined. Aborting...");
                return;
            }

            Star star = new Star
            {
                Name = starSystem.GetType().GetField($"{starName}_Name").GetValue(starSystem).ToString(),

                NumberOfDirectChildren = Convert.ToInt32(starSystem.GetType().GetField($"{starName}_NumberOfDirectChildren").GetValue(starSystem)),
                EquatorialRadius = (float)starSystem.GetType().GetField($"{starName}_AtmospherePresent").GetValue(starSystem),
                Density = (float)starSystem.GetType().GetField($"{starName}_Density").GetValue(starSystem),
                Mass = (float)starSystem.GetType().GetField($"{starName}_Mass").GetValue(starSystem),
                EscapeVelocity = (float)starSystem.GetType().GetField($"{starName}_EscapeVelocity").GetValue(starSystem),
                SiderealRotationPeriod = (float)starSystem.GetType().GetField($"{starName}_SiderealRotationPeriod").GetValue(starSystem),
                SphereOfInfluence = (float)starSystem.GetType().GetField($"{starName}_SphereOfInfluence").GetValue(starSystem),

                AtmospherePresent = Convert.ToBoolean(starSystem.GetType().GetField($"{starName}_AtmospherePresent").GetValue(starSystem)),
                AtmosphericPressure = (float)starSystem.GetType().GetField($"{starName}_AtmosphericPressure").GetValue(starSystem),
                AtmosphericHeight = (float)starSystem.GetType().GetField($"{starName}_AtmosphericHeight").GetValue(starSystem),
                TemperatureMin = (float)starSystem.GetType().GetField($"{starName}_TemperatureMin").GetValue(starSystem),
                TemperatureMax = (float)starSystem.GetType().GetField($"{starName}_TemperatureMax").GetValue(starSystem),
                OxigenPresent = Convert.ToBoolean(starSystem.GetType().GetField($"{starName}_OxigenPresent").GetValue(starSystem))
            };

            for (int i = 1; i <= star.NumberOfDirectChildren; i++)
            {
                int planetNumber = starNumber + 10 * i;
                CreatePlanet(planetNumber);
            }


        }

        private void CreatePlanet(int planetNumber)
        {
            CreateMoon();
        }

        private void CreateMoon()
        {

        }
    }
}
