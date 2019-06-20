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
		public int Id;
		public D2A d2a;
		public Customer Customer;
		public Way Way;
		public decimal Price;
		public string xml;
		/// <summary>
		/// Конструктор экземпляра заказа
		/// </summary>
		/// <param name="ID">ID заказа</param>
		/// <param name="price">Сумма заказа</param>
		/// <param name="way">Экземпляр класса пути</param>
		/// <param name="customer">Экземпляр класса заказчик</param>
		/// <param name="d2a">Экземпляр класса водитель</param>
		/// <param name="x">xml заказа</param>
		public Order(int ID, decimal price, Way way,  Customer customer, D2A d2a, string x = "")
		{
			this.Id = ID;
			this.Price = price;
			this.Way = way;
			this.d2a = d2a;
			this.Customer = customer;
			this.xml = x;
		}
		public Order(){ }
	}
}