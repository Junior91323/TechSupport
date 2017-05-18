using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.BLL.Interfaces;
using TechSupport.BLL.Observers;
using TechSupport.DAL.Interfaces;

namespace TechSupport.BLL.Services
{
    public class UnitOfService : IUnitOfService
    {
        bool Disposed = false;

        IEmployeesService _EmployeeService;
        IRequestsService _RequestService;
        IObserver requestObserver;


        IUnitOfWork DB { get; set; }
        public IEmployeesService EmployeeService
        {
            get
            {
                if (_EmployeeService == null)
                    _EmployeeService = new EmployeesService(DB);

                return _EmployeeService;
            }
        }
        public IRequestsService RequestService
        {
            get
            {
                if (_RequestService == null)
                    _RequestService = new RequestsService(DB);

                return _RequestService;
            }
        }

        public UnitOfService(IUnitOfWork uow)
        {
            this.DB = uow;
            this.requestObserver = new RequestsObserver(DB);

            //if (EmployeeService is IAgregator)
            //    (EmployeeService as IAgregator).RegisterObserver(requestObserver);

            if (RequestService is IAgregator)
                (RequestService as IAgregator).RegisterObserver(this.requestObserver);
        }



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
