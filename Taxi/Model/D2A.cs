using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Taxi.DataBase;
using Taxi.Model;
using Taxi.Model.Person;

namespace Taxi.Model
{
	public class D2A
	{
		public int		Id;
		public Car		Auto;
		public Driver	Driver;
		public DateTime Date;

		public static List<D2A> GetD2As(string connectionString, SqlDataAdapter adapter, List<Car> carList, List<Driver> driverList)
		{
			List<D2A> d2al = new List<D2A>();
			DataTable Table = TableInit.D2AInit();

			adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("Drivers2Auto", connectionString);
			adapter.Fill(Table);
			foreach (DataRow row in Table.Rows)
				d2al.Add(TableInit.D2AGetRow(row, carList, driverList));
			return (d2al);
		}

		public static (List<D2A>, List<Car>, List<Driver>) GetD2As(string connectionString, SqlDataAdapter adapter)
		{
			List<D2A> d2al = new List<D2A>();
			DataTable Table = TableInit.D2AInit();
			List<Car> carList = Car.GetCars(connectionString, adapter);
			List<Driver> driverList = Driver.GetDrivers(connectionString, adapter);

			adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("Drivers2Auto", connectionString);
			adapter.Fill(Table);
			foreach (DataRow row in Table.Rows)
				d2al.Add(TableInit.D2AGetRow(row, carList, driverList));
			return (d2al, carList, driverList);
		}
		public override string ToString()
		{
			return $"{Auto.Mark} {Auto.Model} - {Driver.Name}";
		}
	}
}