using AltaMedia.Core;
using AltaMedia.Helper;
using AltaMedia.Model;
using AltaMedia.Service.AdminBusiness.Models.Input;
using AltaMedia.Service.AdminBusiness.Models.Output;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMedia.Service.AdminBusiness
{
    public class AdminBusiness : IAdminBusiness
    {
        private readonly AltaMediaDbContext _context;

        public AdminBusiness(AltaMediaDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseBase<ReportEventOutput>> ReportEvent(ReportEventInput request)
        {
            if (request.UserId == 0)
            {
                return new ResponseBase<ReportEventOutput>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Không có quyền truy cập"
                };
            }

            var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user.RoleId != (int)UserRole.Admin)
            {
                return new ResponseBase<ReportEventOutput>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Không có quyền truy cập"
                };
            }

            var events = _context.Events.FirstOrDefault(x => x.Id == request.EventId);
            if (events == null)
            {
                return new ResponseBase<ReportEventOutput>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Không có quyền truy cập"
                };
            }

            var selledTicket = _context.Tickets.Where(x => x.EventId == request.EventId && x.Quantity == 0).Count();

            var res = new ReportEventOutput()
            {
                EventName = events.Name,
                TotalTicket = events.TotalTicket,
                SelledTicket = selledTicket
            };

            return new ResponseBase<ReportEventOutput>()
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Báo cáo sự kiện" + events.Name,
                Data = res
            };
        }
    }
}
