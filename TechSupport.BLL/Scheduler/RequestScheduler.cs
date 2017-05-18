using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.BLL.Enumerations;
using TechSupport.DAL.Entities;
using TechSupport.DAL.Interfaces;
using TechSupport.DAL.Repositories;

namespace TechSupport.BLL.Scheduler
{
    public class RequestScheduler : IJob
    {
        private IUnitOfWork DB;
        public RequestScheduler()
        {
            this.DB = new TSUnitOfWork("DefaultConnection");
        }
        public RequestScheduler(IUnitOfWork db)
        {
            this.DB = db;
        }
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                using (DB)
                {
                    List<Request> requests = DB.Requests.GetAll().Where(x => x.StateId == (Int32)RequestStates.New).OrderBy(x => x.CreatedDate).ToList();

                    if (requests != null && requests.Count > 0)
                    {
                        List<Employee> employee = DB.Employees.GetAll().Where(x => x.RequestId == null).ToList();
                        if (employee != null && employee.Count > 0)
                        {
                            List<Employee> operators = employee.Where(x => x.PositionId == (Int32)EmployeePositions.Operator).ToList();
                            foreach (Request request in requests)
                            {
                                Employee _operator = operators.FirstOrDefault();
                                if (_operator != null)
                                {
                                    _operator.RequestId = request.Id;
                                    DB.Employees.Update(_operator);
                                    operators.Remove(_operator);

                                    request.StateId = (Int32)RequestStates.Processing;
                                    DB.Requests.Update(request);
                                    requests.Remove(request);
                                }
                                else { break; }
                            }

                            List<Request> requests2 = requests.Where(x => (DateTime.Now - x.CreatedDate).Seconds > 30).ToList();
                            if (requests2.Count > 0)
                            {
                                List<Employee> managers = employee.Where(x => x.PositionId == (Int32)EmployeePositions.Manager).ToList();
                                foreach (Request request in requests2)
                                {
                                    Employee manager = managers.FirstOrDefault();
                                    if (manager != null)
                                    {
                                        manager.RequestId = request.Id;
                                        DB.Employees.Update(manager);
                                        operators.Remove(manager);

                                        request.StateId = (Int32)RequestStates.Processing;
                                        DB.Requests.Update(request);
                                        requests.Remove(request);
                                    }
                                    else { break; }
                                }
                            }
                            List<Request> requests3 = requests.Where(x => (DateTime.Now - x.CreatedDate).Seconds > 60).ToList();
                            if (requests3.Count > 0)
                            {
                                List<Employee> directors = employee.Where(x => x.PositionId == (Int32)EmployeePositions.Director).ToList();
                                foreach (Request request in requests3)
                                {
                                    Employee director = directors.FirstOrDefault();
                                    if (director != null)
                                    {
                                        director.RequestId = request.Id;
                                        DB.Employees.Update(director);
                                        operators.Remove(director);

                                        request.StateId = (Int32)RequestStates.Processing;
                                        DB.Requests.Update(request);
                                        requests.Remove(request);
                                    }
                                    else { break; }
                                }
                            }

                            DB.Save();
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
    }
}
