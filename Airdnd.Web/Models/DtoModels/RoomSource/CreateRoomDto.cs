using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;

namespace Airdnd.Web.Models.DtoModels.RoomSource
{
	public class CreateRoomDto
	{
		public int PropertyGroupID { get; set; }
		public int PropertyTypeID { get; set; }
		public int PrivacyTypeID { get; set; }
		public string Address { get; set; }
		//public double Lng { get; set; }
		//public double Lat { get; set; }
		public int People { get; set; }
		public int Bed { get; set; }
		public int Bedroom { get; set; }
		public int BathRoom { get; set; }
		//public int Toilet { get; set; }
		public bool InsideBathroom { get; set; }
		public string[] Services { get; set; }
        public string RoomName { get; set; }
		public int HighLightID { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string[] Legals { get; set; }
	}
}
