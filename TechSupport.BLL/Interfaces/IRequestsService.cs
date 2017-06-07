using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.BLL.DTO;

namespace TechSupport.BLL.Interfaces
{
    public interface IRequestsService:IService<RequestDTO>
    {
        RequestDTO GetFirst();
        RequestDTO GetLast();
        void Cancel(int id);
    }
}
