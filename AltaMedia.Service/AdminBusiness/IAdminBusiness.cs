using AltaMedia.Core;
using AltaMedia.Service.AdminBusiness.Models.Input;
using AltaMedia.Service.AdminBusiness.Models.Output;

namespace AltaMedia.Service.AdminBusiness
{
    public interface IAdminBusiness
    {
        Task<ResponseBase<ReportEventOutput>> ReportEvent(ReportEventInput request);
    }
}
