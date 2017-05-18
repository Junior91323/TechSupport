using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.BLL.DTO;
using TechSupport.BLL.Interfaces;
using TechSupport.DAL.Entities;
using TechSupport.DAL.Interfaces;
using TechSupport.BLL.Enumerations;
using AutoMapper;

namespace TechSupport.BLL.Services
{
    public class EmployeesService : IEmployeesService, IAgregator
    {
        IUnitOfWork DB { get; set; }
        List<IObserver> Observers { get; set; }

        public EmployeesService(IUnitOfWork uow)
        {
            DB = uow;
            this.Observers = new List<Interfaces.IObserver>();
        }
        public void CreateEmployee(EmployeeDTO item)
        {
            try
            {
                if (item == null)
                    throw new NullReferenceException("item is null");

                Employee employee = DB.Employees.Get(item.Id);

                if (employee != null)
                    throw new Exception("Employee already exist");

                Employee res = new Employee
                {
                    Name = item.Name,
                    LastName = item.LastName,
                    PositionId = item.PositionId,
                };
                DB.Employees.Create(res);
                DB.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void DeleteEmployee(int id)
        {
            try
            {
                DB.Employees.Delete(id);
                DB.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public EmployeeDTO GetEmployee(int id)
        {
            try
            {
                var item = DB.Employees.Get(id);

                if (item == null)
                    throw new NullReferenceException(String.Format("Item with id: {0} is not found!", id));

                Mapper.Initialize(cfg => { cfg.CreateMap<Employee, EmployeeDTO>(); cfg.CreateMap<Request, RequestDTO>(); });
                return Mapper.Map<Employee, EmployeeDTO>(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            try
            {
                Mapper.Initialize(cfg => { cfg.CreateMap<Employee, EmployeeDTO>().ForMember("Position", opt => opt.MapFrom(src => src.Position.Title)); cfg.CreateMap<Request, RequestDTO>(); });
                return Mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(DB.Employees.GetAll().OrderBy(x => x.Name).ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void NotifyObserver()
        {
            try
            {
                if (Observers != null)
                {
                    foreach (IObserver item in Observers)
                    {
                        item.Update(null);
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex.InnerException); }
        }

        public void RegisterObserver(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver(IObserver observer)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
