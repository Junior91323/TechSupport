using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechSupport.DAL.EF;
using TechSupport.DAL.Entities;
using TechSupport.DAL.Interfaces.Repositories;

namespace TechSupport.DAL.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private TechSupportContext DB;

        public EmployeesRepository(TechSupportContext context)
        {
            this.DB = context;
        }
        public void Create(Employee item)
        {
            if (item != null)
                DB.Employees.Add(item);
        }

        public void Delete(int id)
        {
            Employee item = DB.Employees.Find(id);

            if (item != null)
                DB.Employees.Remove(item);
        }

        public IQueryable<Employee> Find(Expression<Func<Employee, bool>> predicate)
        {
            return DB.Employees.Where(predicate);
        }

        public Employee Get(int id)
        {
            return DB.Employees.Find(id);
        }

        public IQueryable<Employee> GetAll()
        {
            return DB.Employees.Include(x=>x.Request);
        }

        public IEnumerable<Position> GetPositions()
        {
            return DB.Positions;
        }

        public void Update(Employee item)
        {
            if (item != null)
                DB.Entry(item).State = EntityState.Modified;
        }
    }
}
