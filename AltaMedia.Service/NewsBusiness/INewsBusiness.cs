using AltaMedia.Core;
using AltaMedia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMedia.Service.NewsBusiness
{
    public interface INewsBusiness
    {
        Task<ResponseBase<int>> Create(News request);
        Task<ResponseBase<int>> Update(News request);
        Task<ResponseBase<News>> GetById(int id);
        Task<ResponseBase<int>> Delete(int id);
    }
}
