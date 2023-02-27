using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using KSP_OrbitManeuvers.BodyConstants;
using CsvHelper;
using KSP_OrbitManeuvers.Objects;
using CsvHelper.Configuration;
using System.Linq;
using KSP_OrbitManeuvers.Helpers;
using System;

namespace KSP_OrbitManeuvers.Data
{
	class SetupData
	{
		public void WriteBodyParameters_FromWiki_toCsv()
		{
			string baseUrl = "https://wiki.kerbalspaceprogram.com/wiki/";
			var system = new KerbolSystem();

			//create csv with header
			var records = new List<CelestialBody>();

			foreach (string bodyName in system.bodies)
			{
				var body = new CelestialBody();
				records.Add(ParseHtml(CallUrl(baseUrl + bodyName).Result, bodyName));
			}

			WriteToCsv(records);
		}

		private static async Task<string> CallUrl(string fullUrl)
		{
			HttpClient client = new HttpClient();
			var response = await client.GetStringAsync(fullUrl);
			return response;
		}
		private CelestialBody ParseHtml(string html, string bodyName)
		{
			HtmlDocument htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(html);

			var dictionary = new CelestialBodiesDictionary();
			int bodyEnum = dictionary.celestialBodyCodes.FirstOrDefault(x => x.Value == bodyName).Key;
			int bodyType = 0;
			if (bodyEnum % 10000 == 0)
            {
				bodyType = (int)BodyType.UNIVERSE;
            }
			else if (bodyEnum % 1000 == 0)
			{
				bodyType = (int)BodyType.STARSYSTEM;
			}
			else if (bodyEnum % 100 == 0)
			{
				bodyType = (int)BodyType.STAR;
			}
			else if (bodyEnum % 10 == 0)
			{
				bodyType = (int)BodyType.PLANET;
			}
			else if(bodyEnum % 1 == 0)
			{
				bodyType = (int)BodyType.MOON;
			}
			else
            {
				TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine("Body Type not defined. Aborting...");
                System.Windows.Forms.Application.Exit();
            }

			int numberOfChildren = 0;
			switch (bodyName)
            {
				case ("Kerbol"):
					numberOfChildren = 7;
					break;
				case ("Kerbin"):
					numberOfChildren = 2;
					break;
				case ("Eve"):
				case ("Duna"):
					numberOfChildren = 1;
					break;
				case ("Jool"):
					numberOfChildren = 5;
					break;
            }

            // common Characteristics
            BodyType type = (BodyType)bodyType;
            string name = bodyName;
            int numberOfDirectChildren = numberOfChildren;

            HtmlNodeCollection tmp;
            // Physical Characteristics4
            tmp = htmlDoc.DocumentNode.SelectNodes("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Equatorial radius')]]]/td[2]/pan/text()");
            double? test = tmp == null ? null : (double?)double.Parse(tmp.Single().InnerText.Replace("&#8201;", "").Replace(".", ","));


            var test2 = 1;
            //Density = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Density')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
            //Mass = double.Parse
            //    (
            //        htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Mass')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace("×10", "e").Replace(".", ",") +
            //        htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Mass')]]]/td[2]/span/sup/text()").InnerText.Replace("&#8201;", "")
            //    ),
            //EscapeVelocity = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Escape velocity')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
            //SiderealRotationPeriod = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Sidereal rotation period')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
            //SolarDay = (bodyType == 1 || bodyType == 100)
            //?
            //(double?)null
            //: double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Solar day')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
            ////SphereOfInfluence = (bodyName == "Jool" || bodyName == "Eeloo") ?
            ////double.Parse
            ////(
            ////    htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Sphere of influence')]]]/td[2]/span/span/text()").InnerText.Replace("&#8201;", "").Replace("×10", "e").Replace(".", ",") +
            ////    htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Sphere of influence')]]]/td[2]/span/span/sup/text()").InnerText.Replace("&#8201;", "")
            ////)
            ////: double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Sphere of influence')]]]/td[2]/span/span/text()").InnerText.Replace("&#8201;", "").Replace("×10", "e").Replace(".", ",")),

            ////Atomspheric Characteristics
            //AtmospherePresent = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmosphere present')]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes"),
            //AtmosphericPressure = ((htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmosphere present')]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes"))
            //    ?
            //    double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Atmospheric pressure')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ","))
            //    : (double?)null),
            //AtmosphericHeight = ((htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmosphere present')]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes"))
            //                ?
            //                double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmospheric height')]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ","))
            //                : (double?)null),
            //TemperatureMin = ((htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmosphere present')]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes"))
            //                ?
            //                double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[sub[contains(text(), 'min')]]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace("&#160;°C ", "").Replace(".", ","))
            //                : (double?)null),
            //TemperatureMax = ((htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmosphere present')]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes"))
            //                ?
            //                double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[sub[contains(text(), 'max')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace("&#160;°C ", "").Replace(".", ","))
            //                : (double?)null),
            //OxigenPresent = ((htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmosphere present')]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes"))
            //                ?
            //                htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Oxygen present')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes")
            //: (bool?)null),

            //// Orbital Characteristics
            ////            SemiMajorAxis = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Semi-major axis')]]]/td[2]/span/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
            ////            Apoapsis = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Apoapsis')]]]/td[2]/span/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
            ////            Periapsis = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Periapsis')]]]/td[2]/span/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
            ////            OrbitalEccentricity = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Orbital eccentricity')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
            ////            OrbitalInclination = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Orbital inclination')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace("&#160;°\n", "").Replace(".", ",")),
            ////            ArgumentOfPeriapsis = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Argument of periapsis')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace("&#160;°\n", "").Replace(".", ",")),
            ////            LongitudeOfAscendingNode = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Longitude of the ascending node')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace("&#160;°\n", "").Replace(".", ",")),
            ////            MeanAnomalyAtBigBang = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Mean anomaly')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace("&#160;rad ", "").Replace(".", ",")),
            ////            SiderealOrbitalPeriod = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Sidereal orbital period')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
            ////            SynodicOrbitalPeriod = bodyName == "Kerbin"
            ////?
            ////(double?)null
            ////: double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Synodic orbital period')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),




			CelestialBody body = new CelestialBody()
			{
				// common Characteristics
				Type = (BodyType)bodyType,
				Name = bodyName,
				NumberOfDirectChildren = numberOfChildren,

                // Physical Characteristics
                EquatorialRadius = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Equatorial radius')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
                Density = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Density')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
                Mass = double.Parse
                    (
                        htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Mass')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace("×10", "e").Replace(".", ",") +
                        htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Mass')]]]/td[2]/span/sup/text()").InnerText.Replace("&#8201;", "")
                    ),
                EscapeVelocity = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Escape velocity')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
                SiderealRotationPeriod = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Sidereal rotation period')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
                SolarDay = (bodyType == 1 || bodyType == 100)
                ?
                (double?)null
                : double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Solar day')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
                //SphereOfInfluence = (bodyName == "Jool" || bodyName == "Eeloo") ?
                //double.Parse
                //(
                //    htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Sphere of influence')]]]/td[2]/span/span/text()").InnerText.Replace("&#8201;", "").Replace("×10", "e").Replace(".", ",") +
                //    htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Sphere of influence')]]]/td[2]/span/span/sup/text()").InnerText.Replace("&#8201;", "")
                //)
                //: double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Sphere of influence')]]]/td[2]/span/span/text()").InnerText.Replace("&#8201;", "").Replace("×10", "e").Replace(".", ",")),

                //Atomspheric Characteristics
                AtmospherePresent = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmosphere present')]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes"),
                AtmosphericPressure = ((htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmosphere present')]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes"))
                    ?
                    double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Atmospheric pressure')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ","))
                    : (double?)null),
                AtmosphericHeight = ((htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmosphere present')]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes"))
                               ?
                               double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmospheric height')]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ","))
                               : (double?)null),
                TemperatureMin = ((htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmosphere present')]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes"))
                               ?
                               double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[sub[contains(text(), 'min')]]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace("&#160;°C ", "").Replace(".", ","))
                               : (double?)null),
                TemperatureMax = ((htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmosphere present')]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes"))
                               ?
                               double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[sub[contains(text(), 'max')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace("&#160;°C ", "").Replace(".", ","))
                               : (double?)null),
                OxigenPresent = ((htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[contains(text(), ' Atmosphere present')]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes"))
                               ?
                               htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Oxygen present')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",").Contains("Yes")
                : (bool?)null),

                // Orbital Characteristics
                //            SemiMajorAxis = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Semi-major axis')]]]/td[2]/span/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
                //            Apoapsis = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Apoapsis')]]]/td[2]/span/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
                //            Periapsis = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Periapsis')]]]/td[2]/span/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
                //            OrbitalEccentricity = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Orbital eccentricity')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
                //            OrbitalInclination = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Orbital inclination')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace("&#160;°\n", "").Replace(".", ",")),
                //            ArgumentOfPeriapsis = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Argument of periapsis')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace("&#160;°\n", "").Replace(".", ",")),
                //            LongitudeOfAscendingNode = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Longitude of the ascending node')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace("&#160;°\n", "").Replace(".", ",")),
                //            MeanAnomalyAtBigBang = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Mean anomaly')]]]/td[2]/text()").InnerText.Replace("&#8201;", "").Replace("&#160;rad ", "").Replace(".", ",")),
                //            SiderealOrbitalPeriod = double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Sidereal orbital period')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),
                //            SynodicOrbitalPeriod = bodyName == "Kerbin"
                //?
                //(double?)null
                //: double.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Synodic orbital period')]]]/td[2]/span/text()").InnerText.Replace("&#8201;", "").Replace(".", ",")),

            };
			
			return body;
		}

		private void WriteToCsv(List<CelestialBody> records)
		{
			using (var writer = new StreamWriter("parameters.csv"))
			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(records);
			}
		}

		public CelestialBody ReadFromCsv(string bodyname)
        {
			List<CelestialBody> records;
			using (var reader = new StreamReader("C:\\Users\\U13D1LV\\source\\repos\\Privat\\KSP_OrbitManeuvers\\KSP_OrbitManeuvers\\bin\\Debug\\parameters.csv"))
			using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				records = csv.GetRecords<CelestialBody>().ToList();
			}
			CelestialBody body  = records.Where(x => x.Name == bodyname).Single();
			//CelestialBody body = new CelestialBody();
			//var test = typeof(CelestialBody).prop
			return body;
        }
	}
}



