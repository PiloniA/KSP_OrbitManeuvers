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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
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
                AtmospherePresent = Convert.ToBoolean(starSystem.GetType().GetField($"{starName}_AtmospherePresent").GetValue(starSystem))
            };
            CreatePlanet();


        }

        private void CreatePlanet()
        {
            CreateMoon();
        }

        private void CreateMoon()
        {

        }
    }
}
