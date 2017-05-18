using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.DAL.Interfaces;
using TechSupport.DAL.Repositories;

namespace TechSupport.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string ConnectionString;
        public ServiceModule(string connection)
        {
            ConnectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<TSUnitOfWork>().WithConstructorArgument(ConnectionString);
        }
    }
}
