using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.BLL.Interfaces
{
   public interface IService<T>
    {
        void Create(T item);
        void Update(T item);
        T Get(int id);
        void Delete(int id);
        IEnumerable<T> GetList();
    }
}
