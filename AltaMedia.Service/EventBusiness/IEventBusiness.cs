using AltaMedia.Core;
using AltaMedia.Model;

namespace AltaMedia.Service.EventBusiness
{
    public interface IEventBusiness
    {
        Task<ResponseBase<bool>> Create(Event request);
        Task<ResponseBase<bool>> Update(Event request);
        Task<ResponseBase<bool>> Delete(int id);
        Task<ResponseBase<Event>> GetById(int id);
    }
}
