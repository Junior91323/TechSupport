using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.BLL.Enumerations
{
    public enum RequestStates
    {

        Processing = 1,
        New = 2,
        Completed = 3,
        Canceled = 5
    }
    public enum EmployeePositions
    {
        Operator = 1,
        Manager = 2,
        Director = 3
    }
}
