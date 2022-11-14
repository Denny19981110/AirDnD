using System;
using System.Collections.Generic;

namespace Airdnd.Web.Models
{
    public class SpecialPriceDto
    {
        public List<specialPrice> expensiveDays { get; set; }
        public List<specialPrice> cheapDays { get; set; }

    }
    public class specialPrice
    {
        public string datetime { get; set; }
        public decimal price { get; set; }
    }
}
