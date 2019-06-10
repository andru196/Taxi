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

		public static DataTable OrderInit()
		{
			var table = new DataTable("Order");
			table.Columns.Add(new DataColumn("Id"));
			table.Columns.Add(new DataColumn("Phone"));
			table.Columns.Add(new DataColumn("AdressTo"));
			table.Columns.Add(new DataColumn("AdressFrom"));
			table.Columns.Add(new DataColumn("Price"));
			table.Columns.Add(new DataColumn("D2A"));
			table.Columns.Add(new DataColumn("for_driver"));
			table.Columns.Add(new DataColumn("Customer Name"));

			return table;
	}

		public static DataTable	AutoInit()
		{
			var table = new DataTable("Autos");
			table.Columns.Add(new DataColumn("Id"));
			table.Columns.Add(new DataColumn("Mark"));
			table.Columns.Add(new DataColumn("Model"));
			table.Columns.Add(new DataColumn("Comfortable"));
			return table;
		}

		public static DataTable CustomerInit()
		{
			var table = new DataTable("Customer");
			table.Columns.Add(new DataColumn("Phone"));
			table.Columns.Add(new DataColumn("Name"));


			return table;


		}

		public static DataTable DriversInit()
		{
			var table = new DataTable("Driver");
			table.Columns.Add(new DataColumn("FullName"));
			table.Columns.Add(new DataColumn("id"));
			table.Columns.Add(new DataColumn("Phone"));
			table.Columns.Add(new DataColumn("docXml"));
			return table;
		}

		public static DataTable AddressInit()
		{
			var table = new DataTable("address");
			table.Columns.Add(new DataColumn("Id"));
			table.Columns.Add(new DataColumn("street"));
			return table;

		}

		public static DataTable D2AInit()
		{
			var table = new DataTable("D2A");
			table.Columns.Add(new DataColumn("Id"));
			table.Columns.Add(new DataColumn("Auto"));
			table.Columns.Add(new DataColumn("Driver"));
			table.Columns.Add(new DataColumn("Date"));

			return table;
		}
		#region

		public static Car CarGetRow(DataRow dr)
		{
			Car newcar = new Car()
			{
				Id = int.Parse(dr["Id"].ToString()),
				Mark = dr["Mark"].ToString(),
				Model = dr["Model"].ToString(),
				comfLevel =(int.Parse(dr["Comfortable"].ToString())) == 1 ? Comfort.Base : ((int.Parse(dr["Comfortable"].ToString())) == 2 ? Comfort.Comfort : Comfort.Business)
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


		public static Driver DriverGetRow(DataRow dr)
        {
            Driver newDriver = new Driver()
            {
                Name = dr["FullName"].ToString(),
                Id = int.Parse(dr["id"].ToString()),
                Phone = dr["Phone"].ToString(),
                xml = dr["docXml"].ToString()
            };
            return newDriver;
        }

		public static Address AddressGetRow(DataRow dr)
		{
			Address newAddress = new Address()
			{
				street = dr["street"].ToString(),
				id = int.Parse(dr["Id"].ToString())
			};
			return newAddress;


		}


		public static Customer CustomerGetRow(DataRow dr)
		{
			Customer newCustomer = new Customer()
			{
				Phone = dr["Phone"].ToString(),
				Name = dr["Name"].ToString()
			};
			return newCustomer;
		}

		public static (int, string) d2aGetRow(DataRow dataRow)
		{
			string s;
			s = $"{dataRow["Mark"].ToString()} {dataRow["Model"].ToString()} {dataRow["FullName"].ToString()}";
			return (int.Parse(dataRow["Id"].ToString()), s);
		}

		//public static Order OrderGetRow0(DataRow dataRow)
		//{
		//	decimal Price;
		//	Way newway = new Way()
		//	{
		//		From = dataRow["From"].ToString().ToLower(),
		//		To = dataRow["To"].ToString().ToLower()
		//	};
		//	Customer newcust = new Customer()
		//	{
		//		Phone = dataRow["Customer Phone"].ToString(),
		//		Name = dataRow["CUstomer Name"].ToString()
		//	};
		//	Car newcar = null;
		//	Driver newdriver = null;
		//	DateTime dt = DateTime.Today;
		//	if (decimal.TryParse(dataRow["Price"].ToString(), out Price))
		//	{
		//		string comflvl = dataRow["ComName"].ToString().ToLower();
		//		newcar = new Car()
		//		{
		//			comfLevel = comflvl == "база" ? Comfort.Base : (comflvl == "комфорт" ? Comfort.Comfort : Comfort.Business),
		//			Model = dataRow["Model"].ToString().ToLower(),
		//			Mark = dataRow["Mark"].ToString().ToLower()
		//		};
		//		newdriver = new Driver()
		//		{
		//			Phone = dataRow["Driver Phone"].ToString(),
		//			Name = dataRow["Driver Name"].ToString()
		//		};
		//		dt = DateTime.Parse(dataRow["Date"].ToString());
		//	}

		//	Order neworder = new Order(int.Parse(dataRow["Id"].ToString()), Price, newway, newcar, newcust, newdriver, dt);
		//	return neworder;
		//}

		public static Order OrderGetRow(DataRow dataRow, List<D2A> d2, List<Address> al, List<Customer> cl)
		{
			decimal vPrice;
			decimal.TryParse(dataRow["Price"].ToString(), out vPrice);
			var From = int.Parse(dataRow["AdressFrom"].ToString());
			var To = int.Parse(dataRow["AdressTo"].ToString());
			int d = int.Parse(dataRow["D2A"].ToString());
			string s = dataRow["for_driver"].ToString();
			Way newway = new Way(al.Where(a => a.id == From).FirstOrDefault(), al.Where(a => a.id == To).FirstOrDefault());
			D2A da2 = d2.Where(a => a.Id == d).SingleOrDefault();
			string p = dataRow["Phone"].ToString();
			Customer cu = cl.Where(a => a.Phone == p).FirstOrDefault();
			Order neworder = new Order(int.Parse(dataRow["Id"].ToString()), vPrice, newway, cu, da2, dataRow["for_driver"].ToString());
			return neworder;
		}

		public static D2A D2AGetRow(DataRow dr, List<Car> cl, List<Driver> dl)
		{
			
			int driver = int.Parse(dr["Driver"].ToString());
			int auto = int.Parse(dr["Auto"].ToString());
			D2A newd2a = new D2A()
			{
				Driver = dl.Where(v => v.Id == driver).SingleOrDefault(),
				Auto = cl.Where(v => v.Id == auto).SingleOrDefault(),
				Date = DateTime.Parse(dr["Date"].ToString()),
				Id = int.Parse(dr["Id"].ToString())
			};
			return newd2a;
		}

		public static DataRow OrderAddRow(DataTable dataTable, Order ord)
		{
			DataRow newRow = dataTable.NewRow();
			newRow["D2A"] = ord.d2a.Id;
			newRow["Price"] = ord.Price;
			newRow["AdressFrom"] = ord.Way.From.street;
			newRow["AdressTo"] = ord.Way.To.street;
			newRow["Phone"] = ord.Customer.Phone;
			newRow["Customer Name"] = ord.Customer.Name;
			newRow["for_driver"] = ord.xml;
			return newRow;
		}

		#endregion




	}
}