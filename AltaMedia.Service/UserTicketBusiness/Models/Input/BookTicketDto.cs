using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMedia.Service.UserTicketBusiness.Models.Input
{
    public class BookTicketDto
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int Quantity { get; set; }
    }
}
