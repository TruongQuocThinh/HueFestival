using AltaMedia.Core;
using AltaMedia.Model;
using AltaMedia.Service.UserTicketBusiness;
using AltaMedia.Service.UserTicketBusiness.Models.Input;
using AltaMedia.Service.UserTicketBusiness.Models.Output;
using Microsoft.AspNetCore.Mvc;

namespace AltaMedia.Controllers
{
    [ApiController]
    [Route("book-ticket")]
    public class BookTicketController : ControllerBase
    {
        private readonly IUserTicketBusiness _userTicketBusiness;
        public BookTicketController(IUserTicketBusiness userTicketBusiness)
        {
            _userTicketBusiness = userTicketBusiness;
        }

       
        /// <summary>
        /// Đặt vé
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseBase<BookTicketOutput>> BookTicket(BookTicketDto request)
        {
            return await _userTicketBusiness.BookTicket(request);
        }
    }
}
