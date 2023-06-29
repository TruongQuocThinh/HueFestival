using AltaMedia.Core;
using AltaMedia.Model;
using AltaMedia.Service.UserBusiness;
using AltaMedia.Service.UserBusiness.Models.Input;
using Microsoft.AspNetCore.Mvc;

namespace AltaMedia.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Lấy danh sách tài khoản
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<ResponseBase<List<User>>> GetAll()
        {
            return await _userService.GetAll();
        }

        /// <summary>
        /// Lấy tài khoản theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getuser/{id}")]
        public async Task<ResponseBase<User>> GetUser(int id)
        {
            return await _userService.GetUserById(id);
        }

        /// <summary>
        /// Tạo tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ResponseBase<int>> Create([FromBody] User user)
        {
            return await _userService.Create(user);
        }


        /// <summary>
        /// Cập nhật tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ResponseBase<int>> Update([FromBody] User user)
        {
            return await _userService.Update(user);
        }


        /// <summary>
        /// Xóa tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<ResponseBase<int>> Delete(int id)
        {
            return await _userService.Delete(id);
        }


        /// <summary>
        /// Phân quyền tài khoản
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set-role")]
        public async Task<ResponseBase<bool>> SetPermission(UserPermissionDto request)
        {
            return await _userService.SetPermission(request);
        }
    }
}