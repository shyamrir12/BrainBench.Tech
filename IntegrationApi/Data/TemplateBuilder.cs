using System.Text.RegularExpressions;

namespace IntegrationApi.Data
{
	public class TemplateBuilder
	{
		public static string BuildTemplate(string smsTemplate, List<string> lstParameters, string searchStr = "{#var#}")
		{
			//string smsTemplate = "Dear {#var#},    You have successfully register to khanij online please login to system by below credentials,     User Name : {#var#}   Password : {#var#}  CHiMMS, GoCG";
			//string finalMessage = "";
			//string searchStr = "{#var#}";
			int startIndex = 0;
			int tupleIndex = 0;
			List<Tuple<int, int>> occurrences = new List<Tuple<int, int>>();
			//List<string> lst = new List<string>();
			//lst.Add("Sunil Deshalahre");
			//lst.Add("Sunil_Deshalahre");
			//lst.Add("Dgm@123");
			while (startIndex < smsTemplate.Length)
			{
				int index = smsTemplate.IndexOf(searchStr, startIndex);
				if (index == -1)
					break;

				occurrences.Add(new Tuple<int, int>(tupleIndex, index));
				tupleIndex++;
				startIndex = index + searchStr.Length;
			}
			foreach ((int searchIndex, int occurrence) in occurrences)
			{
				//Console.WriteLine("Found at index: [" + searchIndex.ToString() + "]" + occurrence + "\n");

				int index = smsTemplate.IndexOf(searchStr);
				//Console.WriteLine(index);
				if (index >= 0)
				{
					if (lstParameters.Count > 0)
					{
						string modifiedString = smsTemplate.Substring(0, index) + lstParameters[0] +
									smsTemplate.Substring(index + searchStr.Length);
						smsTemplate = modifiedString;
						//Console.WriteLine(modifiedString);
						lstParameters.RemoveAt(0);
					}
				}
			}
			return smsTemplate;
		}

		public static string BuildEmailTemplate(string smsTemplate, List<string> lstParameters, string searchStr = "{#var#}")
		{
			//var templateParts = smsTemplate.Split(searchStr);
			//// In case the {#var#} count in smsTemplate differs from the lst count
			//var replacementCount = Math.Min(lstParameters.Count, templateParts.Length - 1);

			//var builder = new StringBuilder(templateParts[0]);

			//for (var i = 0; i < replacementCount; i++)
			//{
			//    builder.Append(lstParameters[i]);
			//    builder.Append(templateParts[i + 1]);
			//}
			//return builder.ToString();
			int count = 0;
			var result = Regex.Replace(smsTemplate, Regex.Escape(searchStr), match => lstParameters[count++]);
			return result;
		}

	}
}
