using AltaMedia.Core;
using AltaMedia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMedia.Service.TicketBusiness
{
    public interface ITicketBusiness
    {
        Task<ResponseBase<Ticket>> GetById(int id);
        Task<ResponseBase<List<Ticket>>> GetAll(int eventid);
    }
}
