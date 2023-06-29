using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMedia.Service.AdminBusiness.Models.Output
{
    public class ReportEventOutput
    {
        public string EventName { get; set; }
        public int TotalTicket { get; set; }
        public int SelledTicket { get; set; }
    }
}
