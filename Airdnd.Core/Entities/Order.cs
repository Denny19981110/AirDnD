using System;
using System.Collections.Generic;

#nullable disable

namespace Airdnd.Core.Entities
{
    public partial class Order
    {
        public Order()
        {
            Calendars = new HashSet<Calendar>();
            Comments = new HashSet<Comment>();
            OrderDates = new HashSet<OrderDate>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ListingId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public int StayNight { get; set; }
        public int PaymentType { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Infants { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime FinishDate { get; set; }
        public int Status { get; set; }
        public int TranStatus { get; set; }
        public string PayId { get; set; }

        public virtual UserAccount Customer { get; set; }
        public virtual Listing Listing { get; set; }
        public virtual ICollection<Calendar> Calendars { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<OrderDate> OrderDates { get; set; }
    }
}
