using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.BLL.DTO;

namespace TechSupport.BLL.Interfaces
{
    public interface IEmployeesService
    {
        void CreateEmployee(EmployeeDTO item);
        EmployeeDTO GetEmployee(int id);
        void DeleteEmployee(int id);
        IEnumerable<EmployeeDTO> GetEmployees();
        void Dispose();
    }
}
