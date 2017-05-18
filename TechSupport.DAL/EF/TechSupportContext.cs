using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.DAL.Entities;

namespace TechSupport.DAL.EF
{
    public class TechSupportContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Request> Requests { get; set; }
       // public DbSet<EmployeeState> EmployeeState { get; set; }
        public DbSet<RequestState> RequestState { get; set; }

        public TechSupportContext(string connectionString)
            : base(connectionString)
        {
        }
        public TechSupportContext() : base("DefaultConnection")
        {

        }
    }
}
