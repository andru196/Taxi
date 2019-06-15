using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;


namespace Taxi.Model.Person
{
	[Table(Name = "Customer")]
	public class Customer : Person
	{
		[Column(IsPrimaryKey = true, IsDbGenerated = false, Name = "Phone")]
		public  string Phone;
		[Column(Name = "Name")]
		public  string Name;
		public new  string ToString(string s)
		{
			return $"Имя: {Name} {s}Телефон: {Phone}";
		}
	}
}