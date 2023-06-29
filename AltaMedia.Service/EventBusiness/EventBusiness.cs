using AltaMedia.Core;
using AltaMedia.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using IronBarCode;

namespace AltaMedia.Service.EventBusiness
{
    public class EventBusiness : IEventBusiness
    {
        private readonly AltaMediaDbContext _context;
        public EventBusiness(AltaMediaDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseBase<bool>> Create(Event request)
        {
            if (request == null)
            {
                return new ResponseBase<bool>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = false,
                    Message = "Bad request"
                };
            }

            await _context.Events.AddAsync(request);
            await _context.SaveChangesAsync();

            var newEvent = await _context.Events.FirstOrDefaultAsync(x => x.Name == request.Name);
            // create ticket
            var listTicket = new List<Ticket>();
            for (int i = 0; i < request.TotalTicket; i++)
            {
                var stringCode = Guid.NewGuid().ToString();
                var ticket = new Ticket()
                {
                    EventId = newEvent.Id,
                    Code = stringCode,
                    PublishDate = request.PublishDate,
                    Quantity = 1,
                    QRCode = CreateQrCode(stringCode)
                };
                listTicket.Add(ticket);
            }
            await _context.Tickets.AddRangeAsync(listTicket);
            await _context.SaveChangesAsync();

            return new ResponseBase<bool>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = true,
                Message = "Tạo sự kiện thành công"
            };
        }

        public async Task<ResponseBase<bool>> Update(Event request)
        {
            if (request == null)
            {
                return new ResponseBase<bool>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = false,
                    Message = "Bad request"
                };
            }

            var events = await _context.Events.FirstOrDefaultAsync(x => x.Id == request.Id && x.PublishDate <= DateTime.Now);
            if (events == null)
            {
                return new ResponseBase<bool>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = false,
                    Message = "Sự kiện không tồn tại hoặc quá hạn"
                };
            }

            events.Name = request.Name;
            events.Content = request.Content;
            events.PublishDate = request.PublishDate;
            events.TotalTicket = request.TotalTicket;

            await _context.SaveChangesAsync();

            return new ResponseBase<bool>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = true,
                Message = "Cập nhật sự kiện thành công"
            };
        }

        public async Task<ResponseBase<bool>> Delete(int id)
        {
            if (id == 0)
            {
                return new ResponseBase<bool>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Bad request"
                };
            }

            var events = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);

            if (events == null)
            {
                return new ResponseBase<bool>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Không tồn tại sự kiện"
                };
            }

            _context.Events.Remove(events);

            return new ResponseBase<bool>()
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Xóa sự kiện thành công"
            };
        }

        public async Task<ResponseBase<Event>> GetById(int id)
        {
            if (id == 0)
            {
                return new ResponseBase<Event>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Bad request"
                };
            }

            var events = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);

            if (events == null)
            {
                return new ResponseBase<Event>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Không tồn tại sự kiện"
                };
            }

            return new ResponseBase<Event>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = events,
                Message = "Thành công"
            };
        }

        public string CreateQrCode(string qrText)
        {
            GeneratedBarcode Qrcode = IronBarCode.QRCodeWriter.CreateQrCode("https://ironsoftware.com/csharp/barcode/docs/");
            Qrcode.SaveAsPng("QRCode/"+qrText +".png");

            return qrText + ".png";
        }
    }
}
