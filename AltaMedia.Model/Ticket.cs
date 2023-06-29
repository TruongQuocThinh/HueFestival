using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMedia.Model
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Code { get; set; }
        public string QRCode { get; set; }
        public DateTime PublishDate { get; set; }
        public int Quantity { get; set; }
    }
}
