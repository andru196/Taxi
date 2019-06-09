using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi.Model.Person
{
	public class Person
	{
		public int		Id;
		public string	Phone;
		public string	Name;

		public override string ToString()
		{
			return $"Имя: {Name} \nТелефон: {Phone}";
		}
		public  string ToString(string s)
		{
			return $"Имя: {Name} {s}Телефон: {Phone}";
		}
	}
}