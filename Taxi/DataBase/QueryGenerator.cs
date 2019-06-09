using System;
using System.Collections.Generic;
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
						order by case when Price is null then 1 else 2 end
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
	}
}