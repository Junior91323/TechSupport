using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.BLL.DTO
{
   public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int PositionId { get; set; }
        public string Position { get; set; }
        public int RequestId { get; set; }
        public RequestDTO Request { get; set; }
    }
}
