using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taxi.Model.Person;
using Taxi.Model;

namespace Taxi.Model.Order
{
	public class Order
	{
		public int					Id;
		public D2A 					d2a;
		public Customer				Customer;
		public Way					Way;
		public decimal				Price;
		public string				xml;
		/// <summary>
		/// Конструктор экземпляра заказа
		/// </summary>
		/// <param name="i">ID заказа</param>
		/// <param name="p">Сумма заказа</param>
		/// <param name="w">Экземпляр класса пути</param>
		/// <param name="c">Экземпляр класса автомобиля</param>
		/// <param name="cu">Экземпляр класса заказчик</param>
		/// <param name="d">Экземпляр класса водитель</param>
		/// <param name="dt">Дата заказа</param>
		public Order(int i, decimal p, Way w,  Customer cu, D2A d2, string x = "")
		{
			this.Id = i;
			this.Price = p;
			this.Way = w;
			this.d2a = d2;
			this.Customer = cu;
			this.xml = x;
		}
		public Order(){ }
	}
}