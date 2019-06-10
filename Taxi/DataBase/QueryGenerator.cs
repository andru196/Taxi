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
		public static SqlCommand OrdersGenerateSelectQuery(string connectionString)
		{
			var sqlCommand = new SqlCommand(@"
                        select * from  info_order
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

		public static SqlCommand OrderGenerateInsertQuery(string connectionString)
		{
			var sqlCommand = new SqlCommand("NewOrder", new SqlConnection(connectionString));
			sqlCommand.CommandType = CommandType.StoredProcedure;

			sqlCommand.Parameters.Add("@name", SqlDbType.NChar, 0, "CUstomer Name");
			sqlCommand.Parameters.Add("@phone", SqlDbType.NChar, 11, "Customer Phone");
			sqlCommand.Parameters.Add("@idd2a", SqlDbType.Int, 0, "Id");
			sqlCommand.Parameters.Add("@From", SqlDbType.Int, 0, "From");
			sqlCommand.Parameters.Add("@To", SqlDbType.Int, 0, "To");
			SqlParameter param = new SqlParameter("@price", SqlDbType.Money);
			param.SourceColumn = "Price";
			sqlCommand.Parameters.Add(param);

			return sqlCommand;
		}
	}
}