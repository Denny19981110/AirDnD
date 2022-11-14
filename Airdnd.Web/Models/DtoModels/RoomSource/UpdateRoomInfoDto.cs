using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Airdnd.Web.Models.DtoModels.RoomSource
{
	public class UpdateRoomInfoDto
	{
		public int Id { get; set; }
        public string RoomName { get; set; }
		public string RoomDescription { get; set; }
		public int PeopleCount { get; set; }
		public string Address { get; set; }
		public IEnumerable<string> Photos { get; set; }
	}
}
