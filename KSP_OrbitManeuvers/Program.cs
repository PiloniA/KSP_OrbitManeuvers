using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KSP_OrbitManeuvers.Objects;

namespace KSP_OrbitManeuvers
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            var pr = new Program();

            List<string> starSystemNames = new List<string>
            {
                "Sytem1",
                "System2"
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
            CreateStar();
        }

        private void CreateStar()
        {
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
