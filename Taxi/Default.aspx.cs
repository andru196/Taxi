
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
		protected List<Driver> driverList = new List<Driver>();
		protected List<Customer> custList = new List<Customer>();
		protected List<Car> carList = new List<Car>();
		protected List<Address> adList = new List<Address>();
		protected List<D2A> d2aList = new List<D2A>();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//начальный запрос страницы
				fillLists(10, 0);
				//hdnCurrentPage.Value = "0";
			}
			else
			{
				fillLists(10, 0);
			}

			d2aList = d2aList.Where(x => x.Date == DateTime.Today).ToList();
			foreach (var da2el in d2aList)
				newd2a.Items.Insert(0, new ListItem(da2el.ToString(), da2el.Id.ToString()));
		}



		private void fillLists(int take, int skip)
		{
			using (var adapter = new SqlDataAdapter())
			{
				//инициируем таблицу-представление
				var Table = TableInit.DriversInit();
				

				//инициировать строку запроса               
				adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("Drivers" ,ConfigurationManager.ConnectionStrings["tpDb"].ConnectionString);

				adapter.Fill(Table);

				foreach (DataRow row in Table.Rows)
				{
					driverList.Add(TableInit.DriverGetRow(row));
				}

				Table = TableInit.CustomerInit();
				adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("Customer", ConfigurationManager.ConnectionStrings["tpDb"].ConnectionString);
				adapter.Fill(Table);
				foreach (DataRow row in Table.Rows)
					custList.Add(TableInit.CustomerGetRow(row));

				Table = TableInit.AddressInit();
				adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("Address", ConfigurationManager.ConnectionStrings["tpDb"].ConnectionString);
				adapter.Fill(Table);
				foreach (DataRow row in Table.Rows)
					adList.Add(TableInit.AddressGetRow(row));

				Table = TableInit.AutoInit();
				adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("Auto", ConfigurationManager.ConnectionStrings["tpDb"].ConnectionString);
				adapter.Fill(Table);
				foreach (DataRow row in Table.Rows)
					carList.Add(TableInit.CarGetRow(row));

				Table = TableInit.D2AInit();
				adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("Drivers2Auto", ConfigurationManager.ConnectionStrings["tpDb"].ConnectionString);
				adapter.Fill(Table);
				foreach (DataRow row in Table.Rows)
					d2aList.Add(TableInit.D2AGetRow(row, carList, driverList));

				Table = TableInit.OrderInit();
				adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("Orders", ConfigurationManager.ConnectionStrings["tpDb"].ConnectionString);
				adapter.Fill(Table);
				foreach (DataRow row in Table.Rows)
					ordList.Add(TableInit.OrderGetRow(row, d2aList, adList, custList));



				//ordList = ordList
				//	.Skip(skip)
				//	.Take(take)
				//	.ToList();
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
			var byf = driverList.AsEnumerable();

			if (!string.IsNullOrEmpty(fltName.Text))
				byf = byf.Where(x => x.Name.Contains(fltName.Text));

			if (!string.IsNullOrEmpty(fltPhone.Text))
				byf = byf.Where(x => x.Phone.Contains(fltPhone.Text));

			if (!string.IsNullOrEmpty(fltDoc.Text))
				byf = byf.Where(x => x.xml.Contains(fltDoc.Text));

			Driver dr = byf.FirstOrDefault();

			var buf = ordList.AsEnumerable();

			buf = buf.Where(a => a.d2a.Driver == dr);

			ordList = buf
			.OrderBy(x => x.Id)
			.ToList();
			if (byf.Count() == 1)
			{
				tbl_footer.Visible = true;
				drInf.Text = dr.ToString();
				int count = buf.Count();
				drCount.Text = count.ToString();
				drMiddle.Text = ((double)count / (double)buf.GroupBy(x => x.d2a.Date).Count()).ToString();


				btnFilter.Text = "Применить фильтр - " + drCount.Text;
			}
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
					d2a = d2aList.Where(a => a.Id == d2aId).FirstOrDefault(),
					Price = price,
					Customer = new Customer()
					{ Name = newName.Text, Phone = newPhone.Text },
					Way = new Way(newFrom.Text, newTo.Text)
					
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
				var ordTable = TableInit.OrderInit();
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