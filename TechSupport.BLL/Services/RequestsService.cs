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
                    throw new NullReferenceException(String.Format("Request (id:{0}) is not found", id));

                Mapper.Initialize(cfg => cfg.CreateMap<Request, RequestDTO>());
                return Mapper.Map<Request, RequestDTO>(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public RequestDTO GetFirst()
        {
            try
            {
                var item = DB.Requests.GetAll().Where(x => x.StateId == (Int32)RequestStates.New).OrderBy(x => x.CreatedDate).FirstOrDefault();

                if (item == null)
                    throw new NullReferenceException(String.Format("First request is not found!"));

                Mapper.Initialize(cfg => cfg.CreateMap<Request, RequestDTO>());
                return Mapper.Map<Request, RequestDTO>(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public RequestDTO GetLast()
        {
            try
            {
                var item = DB.Requests.GetAll().Where(x => x.StateId == (Int32)RequestStates.New).OrderByDescending(x => x.CreatedDate).FirstOrDefault();

                if (item == null)
                    throw new NullReferenceException(String.Format("Last request is not found!"));

                Mapper.Initialize(cfg => cfg.CreateMap<Request, RequestDTO>());
                return Mapper.Map<Request, RequestDTO>(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IEnumerable<RequestDTO> GetList()
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Request, RequestDTO>());
                return Mapper.Map<IEnumerable<Request>, List<RequestDTO>>(DB.Requests.GetAll().OrderByDescending(x => x.CreatedDate).ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
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
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }

        public void Create(RequestDTO item)
        {
            try
            {
                if (item == null)
                    throw new NullReferenceException("item is null");

                Request request = DB.Requests.Get(item.Id);

                if (request != null)
                    throw new Exception(String.Format("Request (id:{0}) already exist", item.Id));

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
                throw new Exception(ex.Message, ex);
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

        public void Delete(int id)
        {
            try
            {
                var res = DB.Requests.Get(id);
                if (res == null)
                    throw new Exception(String.Format("Request (id:{0}) is not found", id));

                DB.Requests.Delete(id);
                DB.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Update(RequestDTO item)
        {
            try
            {
                if (item == null)
                    throw new NullReferenceException("item is null");

                Request request = DB.Requests.Get(item.Id);

                if (request == null)
                    throw new Exception(String.Format("Request (id:{0}) is not found", item.Id));

                request.Description = item.Description;
                request.StateId = item.StateId;

                DB.Requests.Update(request);
                DB.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Cancel(int id)
        {
            try
            {
                var item = DB.Requests.Get(id);

                if (item == null)
                    throw new Exception(String.Format("Request (id:{0}) is not found", item.Id));

                item.StateId = (Int32)RequestStates.Canceled;

                DB.Requests.Update(item);
                DB.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
