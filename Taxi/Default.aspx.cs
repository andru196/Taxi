
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
using Taxi.Model;
using Taxi.Model.Person;

namespace Taxi
{
	public partial class _Default : Page
	{
		/// <summary>
		/// Список заказов
		/// </summary>
		protected List<Order> ordList = new List<Order>();
		protected List<(int, string)> d2a_List = new List<(int, string)>();
		//protected List<Driver> driverList = new List<Driver>();
		//protected List<Customer> custList = new List<Customer>();
		//protected List<Car> carList = new List<Car>();


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
				fillList(10, 0);
			}
			foreach (var da2el in d2a_List)
				newd2a.Items.Insert(0, new ListItem(da2el.Item2, da2el.Item1.ToString()));
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
		// Реакция на нажатия
		#region

		protected void btnFilter_Click(object sender, EventArgs e)
		{
			var byf = ordList.AsEnumerable();

			if (!string.IsNullOrEmpty(fltName.Text))
				byf = byf.Where(x => x.Driver.Name.Contains(fltName.Text));

			if (!string.IsNullOrEmpty(fltPhone.Text))
				byf = byf.Where(x => x.Driver.Phone.Contains(fltPhone.Text));

			//if (!string.IsNullOrEmpty(fltDoc.Text))
			//	byf = byf.Where(x => x.Driver.Phone.Contains(fltPhone.Text));

			ordList = byf
			.OrderBy(x => x.Id)
			.ToList();

			var count = byf.Count(); //procList.Count

			btnFilter.Text = "Применить фильтр - " + count;
		}

		protected void btnAddNewOrder(object sender, EventArgs e)
		{
			if (newActionType.SelectedValue == "1")
			{
				int d2aId, price;
				int.TryParse(newd2a.SelectedValue, out d2aId);
				int.TryParse(newPrice.Text, out price);
				var newOrd = new Order()
				{
					Id = d2aId,
					Price = price,
					Customer = new Customer()
					{ Name = newName.Text, Phone = newPhone.Text },
					Way = new Way()
					{ From = newFrom.Text, To = newTo.Text },
				};

				addOrdToDb(newOrd);
			}
		}

		#endregion

		// Обработка
		#region

		protected void addOrdToDb(Order ord)
		{
			using (var adapter = new SqlDataAdapter())
			{
				//инициируем таблицу-представление
				var ordTable = TableInit.OrdersInit();
				//генерация новой записи
				var newRow = TableInit.OrderAddRow(ordTable, ord);
				ordTable.Rows.Add(newRow);
				//инициировать строку запроса               
				adapter.InsertCommand = QueryGenerator.OrderGenerateInsertQuery(ConfigurationManager.ConnectionStrings["tpDb"].ConnectionString);

				adapter.Update(ordTable);
			}
		}


		#endregion

	}
}