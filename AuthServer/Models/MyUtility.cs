using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace AuthServer.Models
{
	public static class MyUtility
	{
		public static string Encrypt(string password)
		{
			var provider = MD5.Create();
			string salt = "S0m3R@nd0mSalt";
			byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
			return BitConverter.ToString(bytes).Replace("-", "").ToLower();
		}

		public static List<T> ConvertDataTable<T>(DataTable dt)
		{
			List<T> data = new List<T>();
			foreach (DataRow row in dt.Rows)
			{
				T item = GetItem<T>(row);
				data.Add(item);
			}
			return data;
		}

		public static T GetItem<T>(DataRow dr)
		{
			Type temp = typeof(T);
			T obj = Activator.CreateInstance<T>();

			foreach (DataColumn column in dr.Table.Columns)
			{
				foreach (PropertyInfo pro in temp.GetProperties())
				{
					if (pro.Name == column.ColumnName)
						pro.SetValue(obj, dr[column.ColumnName], null);
					else
						continue;
				}
			}
			return obj;
		}
		public static DataSet ToDataSet<T>(this IList<T> list)
		{
			Type elementType = typeof(T);
			DataSet ds = new DataSet();
			DataTable t = new DataTable();
			ds.Tables.Add(t);

			//add a column to table for each public property on T
			foreach (var propInfo in elementType.GetProperties())
			{
				if (propInfo.PropertyType.Name.Contains("Nullable"))
					t.Columns.Add(propInfo.Name, typeof(String));

				else
					t.Columns.Add(propInfo.Name, propInfo.PropertyType);

			}

			//go through each property on T and add each value to the table
			foreach (T item in list)
			{
				DataRow row = t.NewRow();
				foreach (var propInfo in elementType.GetProperties())
				{
					row[propInfo.Name] = propInfo.GetValue(item, null);
				}
				t.Rows.Add(row);
			}

			return ds;
			//use ds = Utility.ToDataSet(List);
		}

		public static DataSet ConvertDataReaderToDataSet(IDataReader data)
		{
			DataSet ds = new DataSet();
			int i = 0;
			while (!data.IsClosed)
			{
				ds.Tables.Add("Table" + (i + 1));
				ds.EnforceConstraints = false;
				ds.Tables[i].Load(data);
				i++;
			}
			return ds;
		}

	}
}
