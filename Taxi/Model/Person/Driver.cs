using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Taxi.DataBase;

namespace Taxi.Model.Person
{
	public class Driver : Person
	{
		public string xml;

		public static List<Driver> GetDrivers(string connectionString, SqlDataAdapter adapter)
		{
			List<Driver> ld = new List<Driver>();
			DataTable Table = TableInit.DriversInit();

			adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("drivers", connectionString);
			adapter.Fill(Table);
			foreach (DataRow row in Table.Rows)
				ld.Add(TableInit.DriverGetRow(row));

			return (ld);
		}
	}
}