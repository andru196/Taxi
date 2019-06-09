using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi.Model
{
	public class Car
	{
		public string	Mark;
		public string	Model;
		public Comfort	comfLevel;
		public override string ToString()
		{
			return $"{Mark} {Model}\nУровнеь комфорта:\t{comfLevel}";
		}
		public string ToString(string s)
		{
			return $"{Mark} {Model}{s}Уровнеь комфорта: {comfLevel}";
		}
	}

	public enum Comfort
	{
		Base = 0,
		Comfort = 1,
		Business = 2
	}
}