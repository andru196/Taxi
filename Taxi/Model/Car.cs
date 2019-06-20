using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Taxi.DataBase;

namespace Taxi.Model
{
	public class Car
	{
		public int Id;
		public string Mark;
		public string Model;
		public Comfort comfLevel;
		public override string ToString()
		{
			return $"{Mark} {Model}\nУровнеь комфорта:\t{comfLevel}";
		}
		public string ToString(string s)
		{
			return $"{Mark} {Model}{s}Уровнеь комфорта: {comfLevel}";
		}
		/// <summary>
		/// Возвращает список автомобилей из БД
		/// </summary>
		/// <param name="connectionString">Строка соединения</param>
		/// <param name="adapter">SqlDataAdapter</param>
		/// <returns></returns>
		public static List<Car> GetCars(string connectionString, SqlDataAdapter adapter)
		{
			List<Car> lc = new List<Car>();
			DataTable Table = TableInit.AutoInit();

			adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("auto", connectionString);
			adapter.Fill(Table);
			foreach (DataRow row in Table.Rows)
				lc.Add(TableInit.CarGetRow(row));
			return (lc);
		}
	}

	public enum Comfort
	{
		Base = 0,
		Comfort = 1,
		Business = 2
	}
}