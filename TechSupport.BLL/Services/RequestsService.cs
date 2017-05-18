using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.BLL.DTO;
using TechSupport.BLL.Enumerations;
using TechSupport.BLL.Interfaces;
using TechSupport.DAL.Entities;
using TechSupport.DAL.Interfaces;

namespace TechSupport.BLL.Services
{
    public class RequestsService : IRequestsService, IAgregator
    {
        IUnitOfWork DB { get; set; }
        List<IObserver> Observers { get; set; }
        Request CurrentRequest;

        public RequestsService(IUnitOfWork uow)
        {
            DB = uow;
            Observers = new List<IObserver>();
        }
        public void Dispose()
        {
            DB.Dispose();
        }

        public RequestDTO Get(int id)
        {
            try
            {
                var item = DB.Requests.Get(id);

                if (item == null)
                    throw new NullReferenceException(String.Format("Item with id: {0} is not found!", id));

                Mapper.Initialize(cfg => cfg.CreateMap<Request, RequestDTO>());
                return Mapper.Map<Request, RequestDTO>(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public RequestDTO GetFirst()
        {
            try
            {
                var item = DB.Requests.GetAll().Where(x => x.StateId == (Int32)RequestStates.New).OrderBy(x => x.CreatedDate).FirstOrDefault();

                if (item == null)
                    throw new NullReferenceException(String.Format("First new request is not found!"));

                Mapper.Initialize(cfg => cfg.CreateMap<Request, RequestDTO>());
                return Mapper.Map<Request, RequestDTO>(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public RequestDTO GetLast()
        {
            try
            {
                var item = DB.Requests.GetAll().Where(x => x.StateId == (Int32)RequestStates.New).OrderByDescending(x => x.CreatedDate).FirstOrDefault();

                if (item == null)
                    throw new NullReferenceException(String.Format("Last new request is not found!"));

                Mapper.Initialize(cfg => cfg.CreateMap<Request, RequestDTO>());
                return Mapper.Map<Request, RequestDTO>(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<RequestDTO> GetList()
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Request, RequestDTO>());
                return Mapper.Map<IEnumerable<Request>, List<RequestDTO>>(DB.Requests.GetAll().OrderByDescending(x => x.CreatedDate));
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
                        item.Update(CurrentRequest);
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex.InnerException); }
        }

        public void Push(RequestDTO item)
        {
            try
            {
                if (item == null)
                    throw new NullReferenceException("item is null");

                Request request = DB.Requests.Get(item.Id);

                if (request != null)
                    throw new Exception("Request already exist");

                Request res = new Request
                {
                    CreatedDate = DateTime.Now,
                    Description = item.Description,
                    StateId = (Int32)RequestStates.New
                };

                DB.Requests.Create(res);
                DB.Save();

                CurrentRequest = res;

                NotifyObserver();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void RegisterObserver(IObserver observer)
        {
            if (Observers != null)
            {
                Observers.Add(observer);
            }
        }

        public void RemoveObserver(IObserver observer)
        {
            if (Observers != null && Observers.Count > 0)
            {
                if (Observers.Contains(observer))
                    Observers.Remove(observer);
            }
        }
    }
}
