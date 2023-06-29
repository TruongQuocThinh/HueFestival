using AltaMedia.Core;
using AltaMedia.Model;
using AltaMedia.Service.UserBusiness.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMedia.Service.UserBusiness
{ 
    public interface IUserService
    {
        Task<ResponseBase<User>> GetUserById(int id);
        Task<ResponseBase<List<User>>> GetAll();
        Task<ResponseBase<int>> Create(User user);
        Task<ResponseBase<int>> Update(User request);
        Task<ResponseBase<User>> Login(string userName, string password);
        Task<ResponseBase<int>> Delete(int userId);
        Task<ResponseBase<bool>> SetPermission(UserPermissionDto request);
    }
}
