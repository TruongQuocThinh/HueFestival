using AltaMedia.Core;
using AltaMedia.Model;
using AltaMedia.Service.UserBusiness.Models.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AltaMedia.Service.UserBusiness
{
    public class UserService : IUserService
    {
        private readonly AltaMediaDbContext _context;

        public UserService(AltaMediaDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseBase<User>> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            return new ResponseBase<User>()
            {
                Data = user,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<ResponseBase<List<User>>> GetAll()
        {
            var res = await _context.Users.OrderByDescending(x => x.Id).ToListAsync();
            return new ResponseBase<List<User>>()
            {
                Data = res,
                StatusCode = 200,
                Message = "Success"
            };
        }

        public async Task<ResponseBase<int>> Create(User user)
        {
            if (user != null)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return new ResponseBase<int>()
                {
                    Data = 1,
                    StatusCode = 200,
                    Message = "Success"
                };
            }
            return new ResponseBase<int>()
            {
                Data = 1,
                StatusCode = 400,
                Message = "Bad request"
            };
        }

        public async Task<ResponseBase<int>> Update(User request)
        {
            try
            {
                if (request != null)
                {
                    _context.Users.Update(request);
                    await _context.SaveChangesAsync();
                    return new ResponseBase<int>
                    {
                        StatusCode = 200,
                        Data = 1,
                        Message = "Thành công"
                    };
                }
                return new ResponseBase<int>
                {
                    StatusCode = 400,
                    Data = 0,
                    Message = "Dữ liệu không đúng định dạng"
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<int>
                {
                    StatusCode = 400,
                    Data = 0,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseBase<int>> Delete(int userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return new ResponseBase<int>
                {
                    StatusCode = 200,
                    Data = 1,
                    Message = "Thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<int>
                {
                    StatusCode = 400,
                    Data = 0,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseBase<User>> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.Equals(userName) && x.Password.Equals(password) && x.IsActive == true);
            if (user != null && user.Id > 0)
            {
                return new ResponseBase<User>()
                {
                    Data = user,
                    StatusCode = 200,
                    Message = "Success"
                };
            }

            return new ResponseBase<User>()
            {
                Data = new User(),
                StatusCode = 500,
                Message = "Sai tài khoản hoặc mật khẩu"
            };
        }

        public async Task<ResponseBase<bool>> SetPermission(UserPermissionDto request)
        {
            if (request == null)
            {
                return new ResponseBase<bool>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = false,
                    Message = "Bad request"
                };
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.Equals(request.UserName) && x.Password.Equals(request.Password));

            if (user == null || user.IsActive == false)
            {
                return new ResponseBase<bool>()
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Data = false,
                    Message = "Tài khoản bị khóa hoặc không còn tồn tại"
                };
            }

            user.RoleId = (int)request.Role;

            _context.SaveChanges();

            return new ResponseBase<bool>()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = true,
                Message = "Phân quyền thành công"
            };
        }
    }
}