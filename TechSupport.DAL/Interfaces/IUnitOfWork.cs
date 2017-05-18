using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.DAL.Interfaces.Repositories;

namespace TechSupport.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeesRepository Employees { get; }
        IRequestRepository Requests { get; }
        void Save();
    }
}
