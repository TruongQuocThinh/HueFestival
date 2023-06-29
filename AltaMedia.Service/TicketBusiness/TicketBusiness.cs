using AltaMedia.Core;
using AltaMedia.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AltaMedia.Service.TicketBusiness
{
    public class TicketBusiness : ITicketBusiness
    {
        private readonly AltaMediaDbContext _context;
        public TicketBusiness(AltaMediaDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseBase<Ticket>> GetById(int id)
        {
            if (id == 0)
            {
                return new ResponseBase<Ticket>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Bad request"
                };
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);

            if (ticket == null)
            {
                return new ResponseBase<Ticket>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Vé đã được sử dụng"
                };
            }

            return new ResponseBase<Ticket>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = ticket,
                Message = "Thành công"
            };
        }

        public async Task<ResponseBase<List<Ticket>>> GetAll(int eventid)
        {
            var tickets = await _context.Tickets.Where(x => x.EventId == eventid).ToListAsync();
            return new ResponseBase<List<Ticket>>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = tickets,
                Message = "Thành công"
            };
        }
    }
}
