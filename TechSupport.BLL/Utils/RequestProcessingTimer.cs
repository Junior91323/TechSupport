using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.BLL.Enumerations;
using TechSupport.DAL.Entities;
using TechSupport.DAL.Interfaces;

namespace TechSupport.BLL.Utils
{
    public class RequestProcessingTimer
    {
        IUnitOfWork DB;
        Request _Request;
        Employee _Employee;

        public RequestProcessingTimer(IUnitOfWork db, Request request, Employee employee)
        {
            this._Request = request;
            this._Employee = employee;
            this.DB = db;
        }
        public void Start(int interval)
        {
            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(interval);
                TheWork();
            });
        }
        private void TheWork()
        {
            try
            {
                if (DB != null && _Employee != null && _Request != null)
                {
                    _Employee.RequestId = null;
                    DB.Employees.Update(_Employee);

                    _Request.StateId = (Int32)RequestStates.Completed;
                    _Request.EndDate = DateTime.Now;
                    DB.Requests.Update(_Request);

                    DB.Save();
                }
            }
            catch (Exception ex)
            {
                //LOG
            }
        }
    }
}
