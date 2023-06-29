using AltaMedia.Core;
using AltaMedia.Model;
using AltaMedia.Service.NewsBusiness;
using AltaMedia.Service.TicketBusiness;
using Microsoft.AspNetCore.Mvc;

namespace AltaMedia.Controllers
{
    [ApiController]
    [Route("news")]
    public class NewsController : ControllerBase
    {
        private readonly INewsBusiness _newsBusiness;
        public NewsController(INewsBusiness newsBusiness)
        {
            _newsBusiness = newsBusiness;
        }
        

        /// <summary>
        /// Lấy thông tin bài viết
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public async Task<ResponseBase<News>> GetById(int id)
        {
            return await _newsBusiness.GetById(id);
        }

        /// <summary>
        /// Tạo bài viết
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ResponseBase<int>> Create([FromBody] News request)
        {
            return await _newsBusiness.Create(request);
        }

        /// <summary>
        /// Cập nhật bài viết
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ResponseBase<int>> Update([FromBody]News request)
        {
            return await _newsBusiness.Update(request);
        }

        /// <summary>
        /// Xóa bài viết
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("detele/{id}")]
        public async Task<ResponseBase<int>> Delete(int id)
        {
            return await _newsBusiness.Delete(id);
        }
    }
}
