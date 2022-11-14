using Microsoft.VisualBasic;
using System;

namespace Airdnd.Web.Models.DtoModels
{
    public class OrderDetailDto
    {
        public string Roomphoto { get; set; }
        public string Roomname { get; set; }
        public string Roomdescription { get; set; }
        public decimal Cleaningfee { get; set; }
        public decimal Servicecharge { get; set; }
    }
}
