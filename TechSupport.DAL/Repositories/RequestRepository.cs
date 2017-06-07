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
    public class RequestRepository : IRequestRepository
    {
        private TechSupportContext DB;

        public RequestRepository(TechSupportContext context)
        {
            this.DB = context;
        }
        public void Create(Request item)
        {
            if (item != null)
                DB.Requests.Add(item);
        }

        public void Delete(int id)
        {
            Request item = DB.Requests.Find(id);

            if (item != null)
                DB.Requests.Remove(item);
        }

        public IQueryable<Request> Find(Expression<Func<Request, bool>> predicate)
        {
            return DB.Requests.Where(predicate);
        }

        public Request Get(int id)
        {
            return DB.Requests.Find(id);
        }

        public IQueryable<Request> GetAll()
        {
            return DB.Requests;
        }

        public Request GetFirst()
        {
            throw new NotImplementedException();
        }

        public Request GetLast()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestState> GetStates()
        {
            return DB.RequestState;
        }

        public void Update(Request item)
        {
            if (item != null)
                DB.Entry(item).State = EntityState.Modified;
        }
    }
}
