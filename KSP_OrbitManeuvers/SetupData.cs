using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using KSP_OrbitManeuvers.BodyConstants;
using CsvHelper;
using CsvHelper.Configuration;
using System.Reflection;
using System.Linq;
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
			var records = new List<string[]>();

			foreach (string bodyName in system.bodies)
			{
				//string fullUrl = baseUrl + bodyName;
				var parameters = ParseHtml(CallUrl(baseUrl + bodyName).Result);
				//records.Add(parameters);
				WriteToCsv(parameters);
				//add to csv
			}
		}
		//public string Index(string fullUrl)
		//{
		//    var response = CallUrl(fullUrl).Result;
		//    return response;
		//}

		private static async Task<string> CallUrl(string fullUrl)
		{
			HttpClient client = new HttpClient();
			var response = await client.GetStringAsync(fullUrl);
			return response;
		}
		private List<string> ParseHtml(string html)
		{
			HtmlDocument htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(html);

			var test1 = htmlDoc.DocumentNode.SelectNodes("//*[@id='mw-content-text']/div[1]/table/tr/td/a[contains(text(), 'Semi-major axis')]");
			//*[@id='mw-content-text']/div[1]/table/tbody
			//*[@id="mw-content-text"]/div[1]/table/tbody
			string valueString = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div[1]/table/tr[td[a[contains(text(), 'Semi-major axis')]]]/td[2]/span/span/text()").InnerText.Replace("&#8201;", "");
			//*[@id="mw-content-text"]/div[1]/table/tbody/tr[6]/td[2]/span/span
			var test = new List<string>();
			//test.Add(ConvertTable(test1));
			test.Add(valueString);
			test.Add("value2");


			return test;
		}
		private void WriteToCsv(List<string> parameters)
		{
			string csv = String.Join(",", parameters.Select(x => x.ToString()).ToArray());

            using (var writer = new StreamWriter("parameters.csv"))
            using (var cw = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                cw.WriteRecords(csv);
            }

            //using (var stream = new MemoryStream())
            //	using (var writer = new StreamWriter("parameters.csv"))
            //	//using (var reader = new StreamReader(stream))
            //	using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            //	{
            ////		var records = new List<List<string>>
            ////{
            ////	new List<string> { "a1", "a2", "a3" },
            ////	new List<string> { "b1", "b2", "b3" }
            ////};

            //		foreach (var record in parameters)
            //		{
            //                  //foreach (var field in record)
            //                  //{
            //                  csv.WriteField(record);
            //                  //}

            //                  csv.NextRecord();
            //		}

            //		writer.Flush();
            //stream.Position = 0;

            //Console.WriteLine(reader.ReadToEnd());






            //StringBuilder sb = new StringBuilder();
            //         foreach (var link in parameters)
            //         {
            //             sb.AppendLine(link);
            //         }

            //using (var writer = new StreamWriter("parameters.csv"))
            //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            //{
            //	csv.Configuration.RegisterClassMap<CSVMap>();
            //	csv.WriteRecords(parameters);
            //}
            //System.IO.File.AppendAllText("parameters.csv", sb.ToString());
        }

		//public class CSVMap : ClassMap<CSV>
		//{
		//	public CSVMap()
		//	{
		//		AutoMap(CultureInfo.InvariantCulture);
		//		Map(m => m.Header1).ConvertUsing(row => string.Join(",", row.Header1)).Index(0);
		//	}
		//}

		//public class CSV
		//{
		//	public int? Id { get; set; }
		//	public List<string> Header1 { get; set; }
		//}

		private static string ConvertTable(HtmlNode node)
		{
			var thColumns = new List<string>();
			var rows = new List<string>();
			var trs = node.SelectNodes("tr");

			// Attempt to parse thead 
			var theadColValues = ParseTHead(node);
			if (theadColValues.Count > 0)
			{
				thColumns.AddRange(theadColValues);
			}

			//Loop through child nodes of table
			if (trs != null && trs.Count > 0)
			{
				foreach (var row in trs)
				{
					// In the event that thead is not present 
					// and <th>'s are only under a <tr>  
					// Loop through th's (column names)
					var ths = row.SelectNodes("th");
					if (ths != null)
					{
						foreach (var cell in ths)
						{
							thColumns.Add(cell.InnerText);
						}
					}

					//Build list of key value pair 
					var tds = row.SelectNodes("td");
					if (tds != null)
					{
						int idx = 0;
						var rowValues = new List<string>();
						foreach (var cell in tds)
						{
							var columnValue = thColumns[idx];
							rowValues.Add(columnValue + ": " + cell.InnerText);
							++idx;
						}
						rows.Add(string.Join("\n", rowValues));
					}
				}
			}

			// Attempt to parse tbody
			var tBodyValues = ParseTBody(thColumns, node);
			if (tBodyValues.Count > 0)
			{
				rows.Add(string.Join("\n\n", tBodyValues));
				return string.Join("", rows);
			}

			return string.Join("\n\n", rows);
		}

		private static List<string> ParseTHead(HtmlNode node)
		{
			var thColumns = new List<string>();

			var thead = node.SelectSingleNode("thead");
			if (thead != null)
			{
				var theadTrs = thead.SelectNodes("tr");
				//Loop through child nodes of table
				foreach (var row in theadTrs)
				{
					var ths = row.SelectNodes("th");
					if (ths != null)
					{
						foreach (var cell in ths)
						{
							thColumns.Add(cell.InnerText);
						}
					}
				}
			}

			return thColumns;
		}

		private static List<string> ParseTBody(List<string> thColumns, HtmlNode node)
		{
			var tBodyValues = new List<string>();

			var tbody = node.SelectSingleNode("tbody");
			if (tbody != null)
			{
				var trs = tbody.SelectNodes("tr");
				//Loop through child nodes of table
				foreach (var row in trs)
				{
					var rowValues = new List<string>();
					var tds = row.SelectNodes("td");
					if (tds != null)
					{
						int idx = 0;
						foreach (var cell in tds)
						{
							var columnValue = thColumns[idx];
							rowValues.Add(columnValue + ": " + cell.InnerText);
							++idx;
						}
					}
					tBodyValues.Add(string.Join("\n", rowValues));
				}
			}

			return tBodyValues;
		}
	}
}
