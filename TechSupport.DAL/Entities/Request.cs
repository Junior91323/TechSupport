using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.DAL.Interfaces;

namespace TechSupport.DAL.Entities
{
    public class Request : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        //public int EmployeeId { get; set; }
        //[ForeignKey("EmployeeId")]
        //public virtual Employee Employee { get; set; }
        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public virtual RequestState State { get; set; }
    }
}
