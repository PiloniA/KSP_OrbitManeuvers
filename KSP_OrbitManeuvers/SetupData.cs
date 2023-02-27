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
			int bodyType = dictionary.celestialBodyCodes.FirstOrDefault(x => x.Value == bodyName).Key;

			CelestialBody body = new CelestialBody()
			{
				Type = (BodyType)bodyType,
				Name = bodyName,
				NumberOfDirectChildren = 1, // needs to be programmed

			};
			body.Name = bodyName;
			body.Type = BodyType.PLANET;
			body.SemiMajorAxis = long.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Semi-major axis')]]]/td[2]/span/span/text()").InnerText.Replace("&#8201;", ""));


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



