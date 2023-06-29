using AltaMedia.Core;
using AltaMedia.Model;
using AltaMedia.Service.UserBusiness;
using AltaMedia.Service.UserBusiness.Models.Input;
using Microsoft.AspNetCore.Mvc;

namespace AltaMedia.Controllers
{
    [ApiController]
    [Route("access")]
    public class AccessController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccessController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Đăng nhập vào hệ thống
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ResponseBase<User>> Login([FromBody] LoginDto request)
        {
            return await _userService.Login(request.Username, request.Password);
        }
    }
}
