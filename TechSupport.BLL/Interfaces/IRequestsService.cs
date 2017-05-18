using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.BLL.DTO;

namespace TechSupport.BLL.Interfaces
{
    public interface IRequestsService
    {
        void Push(RequestDTO item);
        RequestDTO Get(int id);
        RequestDTO GetFirst();
        RequestDTO GetLast();
        IEnumerable<RequestDTO> GetList();
        void Dispose();
    }
}
