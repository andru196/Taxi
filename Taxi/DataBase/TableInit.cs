using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taxi.Model.Order;
using System.Data;
using Taxi.Model;
using Taxi.Model.Person;

namespace Taxi.DataBase
{
	public class TableInit
	{
		/// <summary>
		/// Инициализация таблицы-представления для списка заказов
		/// </summary>
		public static DataTable OrdersInit()
		{
		var table = new DataTable("Orders");
			//adapter.Fill(ds, "ElectronicProcedure");
			table.Columns.Add(new DataColumn("Id"));
			table.Columns.Add(new DataColumn("Price"));
			table.Columns.Add(new DataColumn("Customer Phone"));
			table.Columns.Add(new DataColumn("To"));
			table.Columns.Add(new DataColumn("From"));
			table.Columns.Add(new DataColumn("CUstomer Name"));
			table.Columns.Add(new DataColumn("Date"));
			table.Columns.Add(new DataColumn("Mark"));
			table.Columns.Add(new DataColumn("Model"));
			table.Columns.Add(new DataColumn("ComName"));
			table.Columns.Add(new DataColumn("Driver Name"));
			table.Columns.Add(new DataColumn("Driver Phone"));
			return table;
		}

		public static DataTable	AutoInit()
		{
			var table = new DataTable("Autos");
			table.Columns.Add(new DataColumn("Id"));
			table.Columns.Add(new DataColumn("Mark"));
			table.Columns.Add(new DataColumn("Model"));
			return table;
		}

		public static Car CarGetRow(DataRow dr)
		{
			Car newcar = new Car()
			{
				Id = int.Parse(dr["Id"].ToString()),
				Mark = dr["Mark"].ToString(),
				Model = dr["Model"].ToString()
			};
			return newcar;
		}
		public static DataTable DaylyDrivers()
		{
			var table = new DataTable("D2A");
			//adapter.Fill(ds, "ElectronicProcedure");
			table.Columns.Add(new DataColumn("Id"));
			table.Columns.Add(new DataColumn("Mark"));
			table.Columns.Add(new DataColumn("Model"));
			table.Columns.Add(new DataColumn("FullName"));
			return table;
		}

		public static (int, string) d2aGetRow(DataRow dataRow)
		{
			string s;
			s = $"{dataRow["Mark"].ToString()} {dataRow["Model"].ToString()} {dataRow["FullName"].ToString()}";
			return (int.Parse(dataRow["Id"].ToString()), s);
		}

		public static Order OrderGetRow(DataRow dataRow)
		{
			decimal Price;
			Way newway = new Way()
			{
				From = dataRow["From"].ToString().ToLower(),
				To = dataRow["To"].ToString().ToLower()
			};
			Customer newcust = new Customer()
			{
				Phone = dataRow["Customer Phone"].ToString(),
				Name = dataRow["CUstomer Name"].ToString()
			};
			Car newcar = null;
			Driver newdriver = null;
			DateTime dt = DateTime.Today;
			if (decimal.TryParse(dataRow["Price"].ToString(), out Price))
			{
				string comflvl = dataRow["ComName"].ToString().ToLower();
				newcar = new Car()
				{
					comfLevel = comflvl == "база" ? Comfort.Base : (comflvl == "комфорт" ? Comfort.Comfort : Comfort.Business),
					Model = dataRow["Model"].ToString().ToLower(),
					Mark = dataRow["Mark"].ToString().ToLower()
				};
				newdriver = new Driver()
				{
					Phone = dataRow["Driver Phone"].ToString(),
					Name = dataRow["Driver Name"].ToString()
				};
				dt = DateTime.Parse(dataRow["Date"].ToString());
			}

			Order neworder = new Order(int.Parse(dataRow["Id"].ToString()), Price, newway, newcar, newcust, newdriver, dt);
			return neworder;
		}

		public static DataRow OrderAddRow(DataTable dataTable, Order ord)
		{
			DataRow newRow = dataTable.NewRow();
			newRow["Id"] = ord.Id;
			newRow["Price"] = ord.Price;
			newRow["From"] = ord.Way.From;
			newRow["To"] = ord.Way.To;
			newRow["Customer Phone"] = ord.Customer.Phone;
			newRow["CUstomer Name"] = ord.Customer.Name;

			return newRow;
		}


	}
}