﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.DAL.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IQueryable<T> GetAll();
        T Get(int id);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
