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
    public class EmployeesService : IEmployeesService
    {
        IUnitOfWork DB { get; set; }

        public EmployeesService(IUnitOfWork uow)
        {
            DB = uow;
        }
        public void Create(EmployeeDTO item)
        {
            try
            {
                if (item == null)
                    throw new NullReferenceException("item is null");

                Employee employee = DB.Employees.Get(item.Id);

                if (employee != null)
                    throw new Exception(String.Format("Employee (id:{0}) already exist", item.Id));

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
                throw new Exception(ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var res = DB.Employees.Get(id);
                if (res == null)
                    throw new Exception(String.Format("Employee (id:{0}) is not found", id));

                DB.Employees.Delete(id);
                DB.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public EmployeeDTO Get(int id)
        {
            try
            {
                var item = DB.Employees.Get(id);

                if (item == null)
                    throw new NullReferenceException(String.Format("Employee (id:{0}) is not found", id));

                Mapper.Initialize(cfg => { cfg.CreateMap<Employee, EmployeeDTO>(); cfg.CreateMap<Request, RequestDTO>(); });
                return Mapper.Map<Employee, EmployeeDTO>(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IEnumerable<EmployeeDTO> GetList()
        {
            try
            {
                Mapper.Initialize(cfg => { cfg.CreateMap<Employee, EmployeeDTO>().ForMember("Position", opt => opt.MapFrom(src => src.Position.Title)); cfg.CreateMap<Request, RequestDTO>(); });
                return Mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(DB.Employees.GetAll().OrderBy(x => x.Name).ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Update(EmployeeDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
