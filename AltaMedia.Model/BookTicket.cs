using System.ComponentModel.DataAnnotations;

namespace AltaMedia.Model
{
    public class BookTicket
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public int TicketId { get; set; }
        public string QRCode { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
