using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.BLL.DTO
{
   public class RequestDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public int StateId { get; set; }
        public string State { get; set; }
    }
}
