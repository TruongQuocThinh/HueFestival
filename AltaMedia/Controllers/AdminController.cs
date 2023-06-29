using AltaMedia.Core;
using AltaMedia.Model;
using AltaMedia.Service.AdminBusiness;
using AltaMedia.Service.AdminBusiness.Models.Input;
using AltaMedia.Service.AdminBusiness.Models.Output;
using AltaMedia.Service.UserBusiness;
using AltaMedia.Service.UserBusiness.Models.Input;
using Microsoft.AspNetCore.Mvc;

namespace AltaMedia.Controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBusiness _adminBusiness;

        public AdminController(IAdminBusiness adminBusiness)
        {
            _adminBusiness = adminBusiness;
        }

        /// <summary>
        /// Báo cáo sự kiện
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("report-event")]
        public async Task<ResponseBase<ReportEventOutput>> ReportEvent([FromBody] ReportEventInput request)
        {
            return await _adminBusiness.ReportEvent(request);
        }
    }
}
