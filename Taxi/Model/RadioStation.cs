using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

namespace Taxi.Model
{
	[Table(Name = "Stations")]
	public class RadioStation
	{
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public int ID { get; set; }
		[Column(Name = "StationName")]
		public string Name { get; set; }
	}
}