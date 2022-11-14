using System;

namespace Airdnd.Admin.Models
{
    public class TransactionDto
    {
        public int OrderId { get; set; }
        public int ListingId { get; set; }
        public int UserAccountId { get; set; }
        public string ListingName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public int TranStatus { get; set; }
    }
}
