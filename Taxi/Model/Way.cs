using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi.Model
{
	public class Way
	{
		public Address	From;
		public Address	To;
		public int		distance;

		public static int DistCount()
		{
			return (0);
		}

		
		public Way(Address f, Address t, int d = 0)
		{
			this.From = f;
			this.To = t;
			this.distance = d;
		}

		public Way(string a, string b)
		{
			this.From = new Address()
			{
				street = a
			};
			this.To = new Address()
			{
				street = b
			};
			

		}
		public override string ToString()
		{
			return $"{From.street} - {To.street}" + (distance == 0 ? "" : ($"\nМаршрут {distance}км."));
		}
		public  string ToString(string s)
		{
			return $"{From.street} -> {To.street}" + (distance == 0 ? "" : ($"{s}Маршрут {distance}км."));
		}
	}
}