using AltaMedia.Core;
using AltaMedia.Model;
using AltaMedia.Service.TicketBusiness;
using Microsoft.AspNetCore.Mvc;

namespace AltaMedia.Controllers
{
    [ApiController]
    [Route("ticket")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketBusiness _ticketBusiness;
        public TicketController(ITicketBusiness ticketBusiness)
        {
            _ticketBusiness = ticketBusiness;
        }

        /// <summary>
        /// Lấy thông tin tất cả vé của sự kiện
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-ticket-event/{eventId}")]
        public async Task<ResponseBase<List<Ticket>>> GetAll(int eventId)
        {
            return await _ticketBusiness.GetAll(eventId);
        }

        /// <summary>
        /// Lấy thông tin vé
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public async Task<ResponseBase<Ticket>> GetById(int id)
        {
            return await _ticketBusiness.GetById(id);
        }
    }
}
