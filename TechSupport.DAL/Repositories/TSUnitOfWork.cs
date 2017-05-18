using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.DAL.EF;
using TechSupport.DAL.Interfaces;
using TechSupport.DAL.Interfaces.Repositories;

namespace TechSupport.DAL.Repositories
{
    public class TSUnitOfWork : IUnitOfWork
    {
        private TechSupportContext DB;
        private EmployeesRepository employeesRepository;
        private RequestRepository requestRepository;

        public IEmployeesRepository Employees
        {
            get
            {
                if (employeesRepository == null)
                    employeesRepository = new EmployeesRepository(DB);
                return employeesRepository;
            }
        }
        public IRequestRepository Requests
        {
            get
            {
                if (requestRepository == null)
                    requestRepository = new RequestRepository(DB);
                return requestRepository;
            }
        }

        public TSUnitOfWork(string connectionString)
        {
            DB = new TechSupportContext(connectionString);
        }

        public void Save()
        {
            try
            {
                DB.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Saving Error", ex.InnerException);
            }
        }

        private bool Disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                    DB.Dispose();
                }
                this.Disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
