
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
using System.Data.Linq;
using System.Xml;

namespace Taxi
{
	public partial class _Default : Page
	{
		/// <summary>
		/// Список заказов
		/// </summary>
		protected List<Order> ordList = new List<Order>();
		protected List<Driver> driverList = new List<Driver>();
		protected List<Customer> custList = new List<Customer>();
		protected List<Car> carList = new List<Car>();
		protected List<Address> adList = new List<Address>();
		protected List<D2A> d2aList = new List<D2A>();
		protected List<RadioStation> rsList;
		protected List<Way> wayList = new List<Way>();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//начальный запрос страницы
				fillLists(10, 0);
				d2aList = d2aList.Where(x => x.Date == DateTime.Today).ToList();
				foreach (var da2el in d2aList)
					newd2a.Items.Insert(0, new ListItem(da2el.ToString(), da2el.Id.ToString()));
				int i = 0;
				rsList.Add(new RadioStation()
				{
					Name = "Не важно",
					ID = 0
				});
				foreach (var rs in rsList)
					newRadio.Items.Insert(i++, new ListItem(rs.Name, rs.ID.ToString()));
				i = 0;
				foreach (var a in adList)
				{
					newFrom.Items.Insert(i, new ListItem(a.street, a.id.ToString()));
					newTo.Items.Insert(i++, new ListItem(a.street, a.id.ToString()));
				}
				//hdnCurrentPage.Value = "0";
			}
			else
			{
				
			}

			
		}


		


		private void fillLists(int take, int skip)
		{
			void littleFiller<cl>(TableInit.Init a, TableInit.GetRow<cl> b, List<cl> lst, string c, SqlDataAdapter adapter, string connectionString1)
			{
				var Table = a();
				adapter.SelectCommand = QueryGenerator.GenerateSelectQuery(c, connectionString1);
				adapter.Fill(Table);
				foreach (DataRow row in Table.Rows)
				{
					lst.Add(b(row));
				}
			}
			var connectionString = ConfigurationManager.ConnectionStrings["tpDb"].ConnectionString;
			using (var adapter = new SqlDataAdapter())
			{
				//инициируем таблицу-представление

				littleFiller(TableInit.DriversInit, TableInit.DriverGetRow, driverList, "Drivers", adapter, connectionString);
				littleFiller(TableInit.CustomerInit, TableInit.CustomerGetRow, custList, "Customer", adapter, connectionString);
				littleFiller(TableInit.AddressInit, TableInit.AddressGetRow, adList, "Address order by street", adapter, connectionString);
				littleFiller(TableInit.AutoInit, TableInit.CarGetRow, carList, "Auto", adapter, connectionString);

				var Table = TableInit.D2AInit();
				adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("Drivers2Auto", connectionString);
				adapter.Fill(Table);
				foreach (DataRow row in Table.Rows)
					d2aList.Add(TableInit.D2AGetRow(row, carList, driverList));

				Table = TableInit.OrderInit();
				adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("Orders", connectionString);
				adapter.Fill(Table);
				foreach (DataRow row in Table.Rows)
					ordList.Add(TableInit.OrderGetRow(row, d2aList, adList, custList));


				Table = TableInit.WayInit();
				adapter.SelectCommand = QueryGenerator.GenerateSelectQuery("Metrics", connectionString);
				adapter.Fill(Table);
				foreach (DataRow row in Table.Rows)
					wayList.Add(TableInit.WayGetRow(row, adList));

				//ordList = ordList
				//	.Skip(skip)
				//	.Take(take)
				//	.ToList();
			}
			//Ещё можно так:
			DataContext db = new DataContext(connectionString);
			 rsList = db.GetTable<RadioStation>().ToList();
		}
		// Реакция на нажатия
		#region

		protected void btnFilter_Click(object sender, EventArgs e)
		{
			var byf = driverList.AsEnumerable();

			if (!string.IsNullOrEmpty(fltName.Text))
				byf = byf.Where(x => x.Name.ToLower().Contains(fltName.Text.ToLower()));

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
				drMiddle.Text = string.Format("{0:f2}", (double)count / (double)buf.GroupBy(x => x.d2a.Date).Count());
				drXML.Text = dr.xml;

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

				XmlDocument xDoc = new XmlDocument();
				xDoc.AppendChild(xDoc.CreateElement("for_driver"));
				xDoc.SelectSingleNode("for_driver").AppendChild(xDoc.CreateElement("radio"));
				xDoc.SelectSingleNode("for_driver/radio").AppendChild(xDoc.CreateTextNode(rsList.Where(x => x.ID == int.Parse(newRadio.SelectedValue)).First().Name));
				xDoc.SelectSingleNode("for_driver").AppendChild(xDoc.CreateElement("extra_info"));
				xDoc.SelectSingleNode("for_driver/extra_info").AppendChild(xDoc.CreateTextNode(newExtra.Text));

				var newOrd = new Order()
				{
					d2a = d2aList.Where(a => a.Id == d2aId).FirstOrDefault(),
					Customer = new Customer()
					{ Name = newName.Text.ToLower(), Phone = newPhone.Text },
					Way = new Way( adList.Where(x => x.id.ToString() == newFrom.SelectedValue).First(), adList.Where(x => x.id.ToString() == newTo.SelectedValue).First()),
					xml = xDoc.InnerXml
				};
				newOrd.Way.Distance = Way.DistCount(newOrd.Way.From, newOrd.Way.To, wayList);
				//price += newOrd.Way.Distance;
				switch (newOrd.d2a.Auto.comfLevel)
				{
					case Comfort.Base:
						price += price * newOrd.Way.Distance * 1 / 10;
						break;
					case Comfort.Comfort:
						price += price * newOrd.Way.Distance * 2 / 10;
						break;
					case Comfort.Business:
						price += price * newOrd.Way.Distance * 5 / 10;
						break;
				}
				newOrd.Price = price;

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