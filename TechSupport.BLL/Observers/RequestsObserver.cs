using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.BLL.Enumerations;
using TechSupport.BLL.Interfaces;
using TechSupport.BLL.Utils;
using TechSupport.DAL.Entities;
using TechSupport.DAL.Interfaces;

namespace TechSupport.BLL.Observers
{
    class RequestsObserver : IObserver
    {
        IUnitOfWork DB { get; set; }

        public RequestsObserver(IUnitOfWork uow)
        {
            DB = uow;
        }

        public void Unsubscribe()
        {
            throw new NotImplementedException();
        }

        public void Update(Request request)
        {
            try
            {
                Employee freeEmployee = DB.Employees.GetAll().Where(x => x.RequestId == null && x.PositionId == (Int32)EmployeePositions.Operator).FirstOrDefault();

                if (freeEmployee != null && request != null)
                {
                    freeEmployee.RequestId = request.Id;
                    freeEmployee.Request = request;
                    DB.Employees.Update(freeEmployee);

                    request.StateId = (Int32)RequestStates.Processing;
                    DB.Requests.Update(request);

                    DB.Save();

                    RequestProcessingTimer timer = new RequestProcessingTimer(DB, request, freeEmployee);
                    timer.Start(30);
                }
            }
            catch (Exception ex)
            {

            }
        }


    }
}
