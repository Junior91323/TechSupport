using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.DAL.Entities;

namespace TechSupport.BLL.Interfaces
{
    public interface IObserver
    {
        void Update(Request request);
        void Unsubscribe();
    }
}
