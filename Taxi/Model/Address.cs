using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi.Model
{
	/// <summary>
	/// Класс адреса
	/// </summary>
    public class Address
    {
        public  int id;
        public  string street;

		public override string ToString()
		{
			return $"{this.id}: {this.street}";
		}
	}
}