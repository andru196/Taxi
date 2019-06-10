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
		public Car		Audo;
		public Driver	Driver;

		public override string ToString()
		{
			return $"{Audo.Mark} {Audo.Model} - {Driver.Name}";
		}
	}
}