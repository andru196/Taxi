
using Taxi.Model.Order;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Taxi.DataBase;

namespace Taxi
{
	public partial class _Default : Page
	{
		/// <summary>
		/// Список заказов
		/// </summary>
		protected List<Order> ordList = new List<Order>();
		protected List<(int, string)> d2a_List = new List<(int, string)>();


		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//начальный запрос страницы
				fillList(10, 0);
				//hdnCurrentPage.Value = "0";
			}
			else
			{
				
			}
			foreach (var da2el in d2a_List)
				d2a.Items.Insert(0, new ListItem(da2el.Item2, da2el.Item1.ToString()));

		}



		private void fillList(int take, int skip)
		{
			using (var adapter = new SqlDataAdapter())
			{
				//инициируем таблицу-представление
				var OrdersTable = TableInit.OrdersInit();
				

				//инициировать строку запроса               
				adapter.SelectCommand = QueryGenerator.OrdersGenerateSelectQuery(ConfigurationManager.ConnectionStrings["tpDb"].ConnectionString);

				adapter.Fill(OrdersTable);

				foreach (DataRow row in OrdersTable.Rows)
				{
					ordList.Add(TableInit.OrderGetRow(row));
				}

				ordList = ordList
					.Skip(skip)
					.Take(take)
					.ToList();
			}
			using (var adapter = new SqlDataAdapter())
			{
				var d2aTable = TableInit.DaylyDrivers();
				adapter.SelectCommand = QueryGenerator.DaylyDriversGenerateSelectQuery(ConfigurationManager.ConnectionStrings["tpDb"].ConnectionString);
				adapter.Fill(d2aTable);
				foreach (DataRow row in d2aTable.Rows)
					d2a_List.Add(TableInit.d2aGetRow(row));
			}
		}
	}
}