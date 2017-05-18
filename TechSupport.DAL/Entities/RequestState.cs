using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.DAL.Interfaces;

namespace TechSupport.DAL.Entities
{
   public class RequestState:IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
