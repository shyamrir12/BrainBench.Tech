using Dapper;
using System.Xml.Serialization;

namespace AdminPanelAPI.Extension
{
	public static class CommonExtension
	{
	
		public static DynamicParameters ToDynamicParameters(this object[] parameters)
		{
			if (parameters.Length % 2 == 1)
				throw new ArgumentException("Invalid parameters");
			DynamicParameters param = new DynamicParameters();
			for (int i = 0; i < parameters.Length / 2; i++)
			{
				param.Add(parameters[2 * i].ToString(), parameters[(2 * i) + 1].ToString());
			}

			return param;
		}
		
		public static DynamicParameters ToDynamicParameters(this object[] parameters, string outParameter)
		{
			if (parameters.Length % 2 == 1)
				throw new Exception("Invalid parameters");
			DynamicParameters param = new DynamicParameters();
			for (int i = 0; i < parameters.Length / 2; i++)
			{
				param.Add(parameters[2 * i].ToString(), parameters[(2 * i) + 1].ToString());
			}
			param.Add(outParameter, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 5215585);
			return param;
		}
		
		public static DynamicParameters ToDynamicParameters(this object[] parameters, string outParameter, int size)
		{
			if (parameters.Length % 2 == 1)
				throw new Exception("Invalid parameters");
			DynamicParameters param = new DynamicParameters();
			for (int i = 0; i < parameters.Length / 2; i++)
			{
				param.Add(parameters[2 * i].ToString(), parameters[(2 * i) + 1].ToString());
			}
			param.Add(outParameter, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: size);
			return param;
		}


		public static string SerializeToXMLString<T>(this T toSerialize)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
			StringWriter textWriter = new StringWriter();

			xmlSerializer.Serialize(textWriter, toSerialize);
			return textWriter.ToString();
		}

	}
}
