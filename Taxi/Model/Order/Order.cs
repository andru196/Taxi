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
		public Driver 				Driver;
		public Customer				Customer;
		public Way					Way;
		public Car					Car;
		public readonly DateTime	Date;
		public decimal				Price;
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
		public Order(int i, decimal p, Way w, Car c, Customer cu, Driver d, DateTime dt)
		{
			this.Id = i;
			this.Price = p;
			this.Way = w;
			this.Car = c;
			this.Customer = cu;
			this.Driver = d;
			this.Date = dt;
		}
	}
}