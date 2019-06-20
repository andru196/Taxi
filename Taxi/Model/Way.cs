using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi.Model
{
	public class Way
	{
		public int Id;
		public Address From;
		public Address To;
		public int Distance;

		public static int DistCount(Address from, Address to,List<Way> lw, List<Address> localAddress = null, int count = 0, int minDistance = 0)
		{
			if (from == to)
				return (0);
			if (localAddress == null)
				(localAddress = new List<Address>()).Add(from);
			if (localAddress.Last() == to)
			{
				localAddress.Remove(localAddress.Last());
				return (count);
			}
			var buf = lw.Where(x => (x.From == localAddress.Last() && !localAddress.Contains(x.To)) ||
										(x.To == localAddress.Last() && !localAddress.Contains(x.From)));
			int min;
			foreach (Way way in buf)
			{
				if (count + way.Distance < minDistance || minDistance == 0)
				{ 
					localAddress.Add(way.From == localAddress.Last() ? way.To : way.From);
					min = DistCount(from, to, lw, localAddress, count + way.Distance, minDistance);
					if ((min < minDistance || minDistance == 0) && min != 0)
						minDistance = min;
				}
			}
			//bool flag = localAddress.Last() == to;
			localAddress.Remove(localAddress.Last());
			//return (flag ? minDistance : 0);
			return minDistance;
		}

		public Way()
		{ }
		public Way(Address f, Address t, int d = 0)
		{
			this.From = f;
			this.To = t;
			this.Distance = d;
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
			return $"{From.street} - {To.street}" + (Distance == 0 ? "" : ($"\nМаршрут {Distance}км."));
		}
		public  string ToString(string s)
		{
			return $"{From.street} -> {To.street}" + (Distance == 0 ? "" : ($"{s}Маршрут {Distance}км."));
		}
	}
}