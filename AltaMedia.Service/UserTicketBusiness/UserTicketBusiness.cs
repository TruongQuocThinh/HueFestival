using AltaMedia.Core;
using AltaMedia.Model;
using AltaMedia.Service.UserTicketBusiness.Models.Input;
using AltaMedia.Service.UserTicketBusiness.Models.Output;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMedia.Service.UserTicketBusiness
{
    public class UserTicketBusiness : IUserTicketBusiness
    {
        private readonly AltaMediaDbContext _context;
        public UserTicketBusiness(AltaMediaDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseBase<BookTicketOutput>> BookTicket(BookTicketDto request)
        {
            if (request == null)
            {
                return new ResponseBase<BookTicketOutput>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Thông tin đặt vé không đúng"
                };
            }

            var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null && user.IsActive == false)
            {
                return new ResponseBase<BookTicketOutput>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Thông tin người đặt không đúng"
                };
            }
            var events = _context.Events.FirstOrDefault(x => x.Id == request.EventId);
            if (events == null || DateTime.Now > events.PublishDate)
            {
                return new ResponseBase<BookTicketOutput>()
                {
                    StatusCode = StatusCodes.Status204NoContent,
                    Message = "Quá hạn đặt vé sự kiện"
                };
            }
            var ticket = _context.Tickets.Where(x => x.EventId == request.EventId).ToList();
            var avaibleTicket = ticket.Where(x => x.Quantity == 1).ToList();
            if (avaibleTicket.Count() < request.Quantity)
            {
                return new ResponseBase<BookTicketOutput>()
                {
                    StatusCode = StatusCodes.Status204NoContent,
                    Message = "Không đủ số lượng vé"
                };
            }

            var bookTickets = new List<BookTicket>();
            var res = new BookTicketOutput();
            for (int i = 0; i < request.Quantity; i++)
            {
                var selectedTicket = avaibleTicket.Skip(i).Take(1);
                var book = new BookTicket()
                {
                    TicketId = selectedTicket.FirstOrDefault().Id,
                    QRCode = selectedTicket.FirstOrDefault().QRCode,
                    CustomerName = user.Name,
                    UserId = user.Id,
                    CreatedDate = DateTime.Now
                };

                bookTickets.Add(book);
                var bookTicketId = bookTickets.Select(x => x.TicketId);
                var listTicketBook = avaibleTicket.Where(x => bookTicketId.Contains(x.Id)).ToList();

                var resetTicket = avaibleTicket.FirstOrDefault(x => x.Id == selectedTicket.First().Id);
                resetTicket.Quantity = 0;

                // set value output
                res.CustomerName = user.Name;
                res.Quantity = request.Quantity;
                res.DateBook = DateTime.Now;
                res.Tickets = listTicketBook;
            }

            _context.BookTickets.AddRange(bookTickets);
            _context.SaveChanges();

            return new ResponseBase<BookTicketOutput>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = res,
                Message = "Đặt vé thành công"
            };
        }
    }
}
