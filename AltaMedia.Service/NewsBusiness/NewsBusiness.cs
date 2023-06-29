using AltaMedia.Core;
using AltaMedia.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMedia.Service.NewsBusiness
{
    public class NewsBusiness : INewsBusiness
    {
        private readonly AltaMediaDbContext _context;

        public NewsBusiness(AltaMediaDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseBase<int>> Create(News request)
        {
            if (request == null)
            {
                return new ResponseBase<int>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Bad request"
                };
            }

            _context.News.Add(request);
            _context.SaveChanges();

            return new ResponseBase<int>()
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Tạo tin tức thành công"
            };
        }

        public async Task<ResponseBase<int>> Update(News request)
        {
            if (request == null)
            {
                return new ResponseBase<int>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Bad request"
                };
            }
            var news = _context.News.FirstOrDefault(x => x.Id == request.Id);
            if (news == null)
            {
                return new ResponseBase<int>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Bài viết không tồn tại"
                };
            }

            news.Title = request.Title;
            news.Content = request.Content;
            news.Image = request.Image;
            _context.SaveChanges();

            return new ResponseBase<int>()
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Cập nhật tin tức thành công"
            };
        }

        public async Task<ResponseBase<News>> GetById(int id)
        {
            var news = await _context.News.FirstOrDefaultAsync(x => x.Id == id);

            if (news == null)
            {
                return new ResponseBase<News>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Bài viết không tồn tại"
                };
            }

            return new ResponseBase<News>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = news,
                Message = "Thành công"
            };
        }

        public async Task<ResponseBase<int>> Delete(int id)
        {
            var news = await _context.News.FirstOrDefaultAsync(x => x.Id == id);

            if (news == null)
            {
                return new ResponseBase<int>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Bài viết không tồn tại"
                };
            }

            _context.News.Remove(news);
            _context.SaveChanges();

            return new ResponseBase<int>()
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Thành công"
            };
        }
    }
}
