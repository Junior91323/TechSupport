using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.DAL.Entities;

namespace TechSupport.DAL.Interfaces.Repositories
{
   public interface IRequestRepository : IRepository<Request>
    {
        IEnumerable<RequestState> GetStates();
        Request GetFirst();
        Request GetLast();
    }
}
