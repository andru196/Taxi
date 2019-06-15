using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Taxi.DataBase
{
	public class QueryGenerator
	{
		//public delegate SqlCommand SomeSqlCommand(string cs);
		public delegate SqlCommand SomeSqlCommand(string cs, UpdateType f = UpdateType.Update);
		//select'ы
		#region
		public static SqlCommand GenerateSelectQuery(string table,string connectionString)
		{
			var sqlCommand = new SqlCommand($" select * from  {table}"
					, new SqlConnection(connectionString));

			return sqlCommand;
		}

		public static SqlCommand AutoGenerateSelectQuery(string connectionString)
		{
			var sqlCommand = new SqlCommand(@"
                        select * from  Auto
                    "
					, new SqlConnection(connectionString));

			return sqlCommand;
		}

		public static SqlCommand DaylyDriversGenerateSelectQuery(string connectionString)
		{
			var sqlCommand = new SqlCommand("select * from d2a_view"
					, new SqlConnection(connectionString));

			return sqlCommand;
		}

		#endregion
		/// <summary>
		/// Генерирует запрс на изменение таблицы Order
		/// </summary>
		/// <param name="connectionString">Строка содинения</param>
		/// <param name="flag"></param>
		/// <returns></returns>
		public static SqlCommand OrderGenerateUpdateQuery(string connectionString, UpdateType up = UpdateType.Update)
		{
			SqlCommand sqlC = new SqlCommand();
			sqlC.Connection = new SqlConnection(connectionString);

			string command = " where [ID] = @id";
			switch (up)
			{
				case UpdateType.Delete:
					command = "delete from orders" + command;
					sqlC.Parameters.Add("@id", SqlDbType.Int, 0, "Id");
					break;
				case UpdateType.Insert:
					command = "NewOrder";
					sqlC.Parameters.Add("@name", SqlDbType.NChar, 0, "Customer Name");
					sqlC.CommandType = CommandType.StoredProcedure;
					break;
				case UpdateType.Update:
				command = "update orders SET " +
						"[Phone] = @phone, " +
						"[AdressTo] = @To, " +
						"[AdressFrom] = @From, " +
						"[Price] = @price, " +
						"[D2A] = @idd2a, " +
						"[for_driver] = @for_driver" + command;
				sqlC.Parameters.Add("@id", SqlDbType.Int, 0, "Id");
					break;
			}

			sqlC.CommandText = command;
			if (up != UpdateType.Delete)
			{
				sqlC.Parameters.Add("@phone", SqlDbType.NVarChar, 11, "Phone");
				sqlC.Parameters.Add("@idd2a", SqlDbType.Int, 0, "D2A");
				sqlC.Parameters.Add("@From", SqlDbType.NChar, 0, "AdressFrom");
				sqlC.Parameters.Add("@To", SqlDbType.NChar, 0, "AdressTo");
				SqlParameter param = new SqlParameter("@price", SqlDbType.Money);
				param.SourceColumn = "Price";
				sqlC.Parameters.Add(param);
				sqlC.Parameters.Add("@for_driver", SqlDbType.Xml, 0, "for_driver");
			}
			return (sqlC);
		}
		//insert'ы
		#region
		public static SqlCommand OrderGenerateInsertQuery(string connectionString)
		{
			var sqlCommand = new SqlCommand("NewOrder", new SqlConnection(connectionString));
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Add("@name", SqlDbType.NChar, 0, "Customer Name");
			sqlCommand.Parameters.Add("@phone", SqlDbType.NChar, 11, "Phone");
			sqlCommand.Parameters.Add("@idd2a", SqlDbType.Int, 0, "D2A");
			sqlCommand.Parameters.Add("@From", SqlDbType.NChar, 0, "AdressFrom");
			sqlCommand.Parameters.Add("@To", SqlDbType.NChar, 0, "AdressTo");
			SqlParameter param = new SqlParameter("@price", SqlDbType.Money);
			param.SourceColumn = "Price";
			sqlCommand.Parameters.Add(param);
			sqlCommand.Parameters.Add("@for_driver", SqlDbType.Xml, 0, "for_driver");


			return sqlCommand;
		}
		
		public static SqlCommand D2AGenerateInsertQuery(string connectionString)
		{
			SqlCommand sqlCommand = new SqlCommand("INSERT INTO drivers2auto (driver, Auto) VALUES (@driver, @auto)", new SqlConnection(connectionString));
			sqlCommand.Parameters.Add("@driver", SqlDbType.Int, 0, "driver");
			sqlCommand.Parameters.Add("@auto", SqlDbType.Int, 0, "Auto");
			return sqlCommand;
		}

		public static SqlCommand AutoGenerateInsertQuery(string connectionString)
		{
			var sqlCommand = new SqlCommand("INSERT INTO Auto (Id, Mark, Model) values (@id, @mark, @model)", new SqlConnection(connectionString));
			sqlCommand.Parameters.Add("@id", SqlDbType.NChar, 0, "Id");
			sqlCommand.Parameters.Add("@mark", SqlDbType.NChar, 0, "Mark");
			sqlCommand.Parameters.Add("@model", SqlDbType.NChar, 0, "Model");
			return sqlCommand;
		}

		public static SqlCommand CustomerGenerateInsertQuery(string connectionString)
		{
			var sqlCommand = new SqlCommand("INSERT INTO Customer (Phone, Name) values (@phone, @name)", new SqlConnection(connectionString));
			sqlCommand.Parameters.Add("@phone", SqlDbType.NChar, 0, "Phone");
			sqlCommand.Parameters.Add("@name", SqlDbType.NChar, 0, "Name");
			return sqlCommand;
		}

		public static SqlCommand DriversGenerateInsertQuery(string connectionString)
		{
			var sqlCommand = new SqlCommand("INSERT INTO driver (FullName,id, phone, docXml) values (@FullName, @id, @phone, @docXml)", new SqlConnection(connectionString));
			sqlCommand.Parameters.Add("@FullName", SqlDbType.NChar, 0, "FullName");
			sqlCommand.Parameters.Add("@id", SqlDbType.NChar, 0, "id");
			sqlCommand.Parameters.Add("@phone", SqlDbType.NChar, 0, "Phone");
			sqlCommand.Parameters.Add("@docXml", SqlDbType.NChar, 0, "docXml");
			return sqlCommand;
		}

		public static SqlCommand AddressGenerateInsertQuery(string connectionString)
		{
			var sqlCommand = new SqlCommand("INSERT INTO address (street) values (@street)", new SqlConnection(connectionString));
			sqlCommand.Parameters.Add("@street", SqlDbType.NChar, 0, "street");
			return sqlCommand;
		}
		#endregion
	}

}