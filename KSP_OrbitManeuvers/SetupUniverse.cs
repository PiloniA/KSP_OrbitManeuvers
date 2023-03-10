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
using KSP_OrbitManeuvers.Data;

namespace KSP_OrbitManeuvers
{
    public class SetupUniverse
    {
        [STAThread]
        public static void Main()
        {
            //var sd = new SetupData();
            //sd.WriteBodyParameters_FromWiki_toCsv();

            var pr = new SetupUniverse();
            CelestialBody universe = pr.CreateUniverse();
            //pr.PrintUniverse(universe);
            CelestialBody kerbol = universe.Children.Where(x => x.Name == "Kerbolsystem").Single().Children.Where(x => x.Name == "Kerbol").Single();
            CelestialBody eve = universe.Children.Where(x => x.Name == "Kerbolsystem").Single().Children.Where(x => x.Name == "Kerbol").Single().Children.Where(x => x.Name == "Eve").Single();
            CelestialBody kerbin = universe.Children.Where(x => x.Name == "Kerbolsystem").Single().Children.Where(x => x.Name == "Kerbol").Single().Children.Where(x => x.Name == "Kerbin").Single();
            double valocityAP = CalculateOrbitParameters.CalculateVelocity_At_Apoapsis(eve, 70000, 70000);
            //double period = CalculateOrbitParameters.CalculatePeriod(eve, 70000, 70000);

            //calculate half orbit of Eve around Sun
            double periodEve = (double)eve.SiderealOrbitalPeriod;
            //calculate time grom kerbin ap to eve pe
            double transferTime = CalculateOrbitParameters.CalculatePeriod(kerbol, (double)kerbin.Apoapsis, (double)eve.Periapsis) / 2;
            //calculate eve traverse angle within transferTime
            // 360° in periodEve
            // x°   in transferTime
            // x = 360/periodEve * transferTime
            double traverseAngle = 360 / periodEve * transferTime;

        }

        private CelestialBody CreateUniverse()
        {
            SetupData setup = new SetupData();
            CelestialBody universe = new CelestialBody
            {
                NumberOfDirectChildren = 1
            };
            foreach (int systemCnt in Enumerable.Range(1,universe.NumberOfDirectChildren))
            {
                int systemEnum = systemCnt * 1000;
                string systemName = new CelestialBodiesDictionary().celestialBodyCodes[systemEnum];
                CelestialBody starsystem = new CelestialBody()
                {
                    Name = systemName,
                    NumberOfDirectChildren = 1
                };

                foreach (int starCnt in Enumerable.Range(1, starsystem.NumberOfDirectChildren))
                {
                    int starEnum = starCnt * 100 + systemEnum;
                    string starName = new CelestialBodiesDictionary().celestialBodyCodes[starEnum];
                    CelestialBody star = setup.ReadFromCsv(starName);

                    foreach (int planetCnt in Enumerable.Range(1, star.NumberOfDirectChildren))
                    {
                        int planetEnum = planetCnt * 10 + starEnum;
                        string planetName = new CelestialBodiesDictionary().celestialBodyCodes[planetEnum];
                        CelestialBody planet = setup.ReadFromCsv(planetName);
                        foreach (int moonCnt in Enumerable.Range(1, planet.NumberOfDirectChildren))
                        {
                            int moonEnum = moonCnt * 1 + planetEnum;
                            string moonName = new CelestialBodiesDictionary().celestialBodyCodes[moonEnum];
                            CelestialBody moon = setup.ReadFromCsv(moonName);
                            planet.Children.Add(moon);
                        }
                        star.Children.Add(planet);
                    }
                    starsystem.Children.Add(star);
                }
                universe.Children.Add(starsystem);
            }

            return universe;
        }

        private void PrintUniverse(CelestialBody universe)
        {
            TextWriter outWriter = Console.Out;

            foreach (CelestialBody starSystem in universe.Children)
            {
                outWriter.WriteLine($"StarSystem Name: {starSystem.Name}");
                foreach (CelestialBody star in starSystem.Children)
                {
                    outWriter.WriteLine($"\tStar Name: {star.Name}");
                    foreach (CelestialBody planet in star.Children)
                    {
                        outWriter.WriteLine($"\t\tPlanet Name: {planet.Name}");
                        foreach (CelestialBody moon in planet.Children)
                        {
                            outWriter.WriteLine($"\t\t\tMoon Name: {moon.Name}");
                        }
                    }
                }
            }
        }

        
    }
}
