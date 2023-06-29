using AltaMedia.Core;
using AltaMedia.Model;
using AltaMedia.Service.EventBusiness;
using AltaMedia.Service.UserBusiness;
using AltaMedia.Service.UserBusiness.Models.Input;
using Microsoft.AspNetCore.Mvc;

namespace AltaMedia.Controllers
{
    [ApiController]
    [Route("event")]
    public class EventController : ControllerBase
    {
        private readonly IEventBusiness _eventBusiness;

        public EventController(IEventBusiness eventBusiness)
        {
            _eventBusiness = eventBusiness;
        }
        
        /// <summary>
        /// Lấy thông tin sự kiện
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public async Task<ResponseBase<Event>> Get(int id)
        {
            return await _eventBusiness.GetById(id);
        }

        /// <summary>
        /// Tạo sự kiện
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ResponseBase<bool>> Create([FromBody] Event request)
        {
            return await _eventBusiness.Create(request);
        }

        /// <summary>
        /// Cập nhật sự kiện
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ResponseBase<bool>> Update([FromBody] Event request)
        {
            return await _eventBusiness.Update(request);
        }

        /// <summary>
        /// Xóa sự kiện
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<ResponseBase<bool>> Delete(int id)
        {
            return await _eventBusiness.Delete(id);
        }
    }
}