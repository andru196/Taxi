using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taxi.Model;
using Taxi.Model.Person;

namespace Taxi.Model
{
	public class D2A
	{
		public int		Id;
		public Car		Auto;
		public Driver	Driver;
		public DateTime Date;
		public override string ToString()
		{
			return $"{Auto.Mark} {Auto.Model} - {Driver.Name}";
		}
	}
}