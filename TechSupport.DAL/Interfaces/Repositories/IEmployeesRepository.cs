using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.DAL.Entities;

namespace TechSupport.DAL.Interfaces.Repositories
{
    public interface IEmployeesRepository : IRepository<Employee>
    {
        IEnumerable<Position> GetPositions();
    }
}
