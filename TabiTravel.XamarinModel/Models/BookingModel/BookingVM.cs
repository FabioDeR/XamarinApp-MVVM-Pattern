using System;
using System.Collections.Generic;
using System.Text;

namespace TabiTravel.XamarinModel.Models.BookingModel
{
    public class BookingVM
    {
        public DateTime AcceptedDate { get; set; }
        public DateTime RefusedDate { get; set; }
        public Guid UserId { get; set; }
        public int BookingId { get; set; }
    }
}
