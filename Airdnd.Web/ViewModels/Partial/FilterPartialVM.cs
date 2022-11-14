using Airdnd.Web.WebApi;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Airdnd.Web.ViewModels.Partial
{
    public class FilterPartialVM
    {
        public IEnumerable<Privacy> Privacy { get; set; }
        public IEnumerable<Property> Property { get; set; }
        public IEnumerable<Property> FillFour { get; set; }
        public IEnumerable<ServiceGroup> Service { get; set; }
        public FilterPrice Price { get; set; }
    }
    public class FilterPartialDto
    {
        public FilterPrice Price { get; set; }
        public IEnumerable<int> Privacies { get; set; }
        [FromQuery(Name = "Rooms")]
        public Rooms Rooms { get; set; }
        [FromQuery(Name = "Properties[]")]
        public IEnumerable<int> Properties { get; set; }
        [FromQuery(Name = "Services[]")]
        public IEnumerable<int> Services { get; set; }

    }
    public class Privacy
    {
        public int Id { get; set; }
        public string IdName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
    public class Rooms
    {
        [FromQuery(Name = "[Bedrooms]")]
        public int Bedrooms { get; set; }
        [FromQuery(Name = "[Beds]")]
        public int Beds { get; set; }
        [FromQuery(Name = "[Bathrooms]")]
        public int Bathrooms { get; set; }
    }
    public class Property
    {
        public int Id { get; set; }
        public string IdName { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
    }
    public class ServiceGroup
    {
        public string Key { get; set; }
        public IEnumerable<ServiceFilter> Service { get; set; }
    }
    public class ServiceFilter
    {
        public string Key { get; set; }
        /// <summary>
        /// V-model用
        /// </summary>
        public string Group { get; set; }
        public int Id { get; set; }
        public string IdName { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; }
    }

    public class FilterPrice
    {
        [FromQuery(Name = "[maxPrice]")]
        public decimal MaxPrice { get; set; }
        [FromQuery(Name = "[minPrice]")]
        public decimal MinPrice { get; set; }
        public decimal PriceAvg { get; set; }
        [FromQuery(Name = "[currentMax]")]
        public decimal CurrentMax { get; set; }
        [FromQuery(Name = "[currentMin]")]
        public decimal CurrentMin { get; set; }
    }

}
