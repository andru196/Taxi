using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Taxi.DataBase;

namespace Taxi.Model
{
	/// <summary>
	/// Класс адреса
	/// </summary>
    public class Address
    {
        public  int id;
        public  string street;

		public override string ToString()
		{
			return $"{this.id}: {this.street}";
		}
		/// <summary>
		/// Генерирует список адрессов в соответствии с базой данных
		/// </summary>
		/// <param name="connectionString">Строка подключений</param>
		/// <param name="adapter">SqlDataAdapter  для заполнения DataSet</param>
		/// <returns></returns>
		public static List<Address>GetAddresses(string connectionString, SqlDataAdapter adapter)
		{
			List<Address> la = new List<Address>();
			DataTable Table = TableInit.AddressInit();

			adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("Address order by street", connectionString);
			adapter.Fill(Table);
			foreach (DataRow row in Table.Rows)
				la.Add(TableInit.AddressGetRow(row));
			return (la);
		}
	}
}