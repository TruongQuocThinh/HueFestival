using AltaMedia.Core;
using AltaMedia.Service.UserTicketBusiness.Models.Input;
using AltaMedia.Service.UserTicketBusiness.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMedia.Service.UserTicketBusiness
{
    public interface IUserTicketBusiness
    {
        Task<ResponseBase<BookTicketOutput>> BookTicket(BookTicketDto request);
    }
}
