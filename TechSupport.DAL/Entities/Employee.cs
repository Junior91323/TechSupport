using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.DAL.Interfaces;

namespace TechSupport.DAL.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        //public int StateId { get; set; }
        //[ForeignKey("StateId")]
        //public virtual EmployeeState State { get; set; }
        public int PositionId { get; set; }
        [ForeignKey("PositionId")]
        public virtual Position Position { get; set; }
        public int? RequestId { get; set; }
        [ForeignKey("RequestId")]
        public virtual Request Request { get; set; }
    }
}
