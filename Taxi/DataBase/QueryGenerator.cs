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