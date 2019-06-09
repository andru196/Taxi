using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi.Model
{
	public class Way
	{
		public string	From;
		public string	To;
		public int		distance;
		public override string ToString()
		{
			return $"{From} - {To}" + (distance == 0 ? "" : ($"\nМаршрут {distance}км."));
		}
		public  string ToString(string s)
		{
			return $"{From} -> {To}" + (distance == 0 ? "" : ($"{s}Маршрут {distance}км."));
		}
	}
}