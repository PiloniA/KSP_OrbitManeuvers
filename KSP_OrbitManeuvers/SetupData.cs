using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using KSP_OrbitManeuvers.BodyConstants;
using CsvHelper;
using KSP_OrbitManeuvers.Objects;

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
				records.Add(ParseHtml(CallUrl(baseUrl + bodyName).Result, body));
			}

			WriteToCsv(records);
		}

		private static async Task<string> CallUrl(string fullUrl)
		{
			HttpClient client = new HttpClient();
			var response = await client.GetStringAsync(fullUrl);
			return response;
		}
		private CelestialBody ParseHtml(string html, CelestialBody body)
		{
			HtmlDocument htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(html);

			body.SemiMajorAxis = long.Parse(htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Semi-major axis')]]]/td[2]/span/span/text()").InnerText.Replace("&#8201;", ""));
			body.Name = "TestName";
			body.Type = BodyType.PLANET;

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
	}
}


