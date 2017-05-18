using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.BLL.Interfaces
{
    public interface IUnitOfService : IDisposable
    {
        IEmployeesService EmployeeService { get; }
        IRequestsService RequestService { get; }
    }
}
