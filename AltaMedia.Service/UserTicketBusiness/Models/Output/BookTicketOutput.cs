using AltaMedia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMedia.Service.UserTicketBusiness.Models.Output
{
    public class BookTicketOutput
    {
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        public DateTime DateBook { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
