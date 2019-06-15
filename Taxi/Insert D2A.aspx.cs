using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taxi.DataBase;
using Taxi.Model;
using Taxi.Model.Person;

namespace Taxi
{
	public partial class Insert_D2A : System.Web.UI.Page
	{
		protected List<Driver> driverList;
		protected List<Car> carList;
		protected List<D2A> d2aList;
		protected void Page_Load(object sender, EventArgs e)
		{
			fillLists();
			if (!IsPostBack)
			{
				//начальный запрос страницы
				int i = 0;
				foreach (Car c in carList)
					AutoId.Items.Insert(i++, new ListItem($"{c.Mark} {c.Model}", c.Id.ToString()));
				i = 0;
				foreach (Driver d in driverList)
					DriverId.Items.Insert(i++, new ListItem(d.Name, d.Id.ToString()));
				//hdnCurrentPage.Value = "0";
			}
		}

		private void fillLists()
		{
			using (var adapter = new SqlDataAdapter())
				(d2aList, carList, driverList) = D2A.GetD2As(ConfigurationManager.ConnectionStrings["tpDb"].ConnectionString, adapter);
			d2aList = d2aList.OrderByDescending(x => x.Date).ToList();
		}


		protected void btnAddNewD2A(object sender, EventArgs e)
		{
			D2A d2a = new D2A()
			{
				Driver = driverList.Where(d => d.Id.ToString() == DriverId.SelectedValue).First(),
				Auto = carList.Where(c => c.Id.ToString() == AutoId.SelectedValue).First()
			};
			if (d2aList.Where(x => x.Date == DateTime.Today && (x.Driver == d2a.Driver || x.Auto == d2a.Auto)).Count() > 0)
				Response.Write("<script>window.alert('Кажется данная машина или водитель уже работают сегодня... Жалко что нельзя заставить работать человека сразу в двух местах');</script>");
			else
				addD2AToBD(d2a);
		}


		protected void addD2AToBD(D2A d2a)
		{
			using (var adapter = new SqlDataAdapter())
			{
				DataTable d2aTable = TableInit.D2AInit();
				DataRow dr = TableInit.D2AAddRow(d2aTable, d2a);
				d2aTable.Rows.Add(dr);
				adapter.InsertCommand = QueryGenerator.D2AGenerateInsertQuery(ConfigurationManager.ConnectionStrings["tpDb"].ConnectionString);

				adapter.Update(d2aTable);
			}
		}
	}
}