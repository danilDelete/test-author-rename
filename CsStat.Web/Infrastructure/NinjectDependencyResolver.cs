using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessFacade.Repositories;
using BusinessFacade.Repositories.Implementations;
using CsStat.Domain.Entities;
using CsStat.LogApi;
using CsStat.LogApi.Interfaces;
using DataService;
using DataService.Interfaces;
using ErrorLogger;
using Ninject;

namespace CSStat.WebApp.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<ICsLogsApi>().To<CsStat.LogApi.CsLogsApi>();
            _kernel.Bind<IMongoRepositoryFactory>().To<MongoRepositoryFactory>();
            _kernel.Bind<IConnectionStringFactory>().To<ConnectionStringFactory>();
            _kernel.Bind<ILogsRepository>().To<LogsRepository>();
            _kernel.Bind<IPlayerRepository>().To<PlayerRepository>();
            _kernel.Bind<IErrorLogRepository>().To<ErrorLogRepository>();
            _kernel.Bind<ISteamApi>().To<SteamApi>();
            _kernel.Bind<ILogger>().To<Logger>();
            _kernel.Bind<IUsefulLinkRepository>().To<UsefulLinkRepository>();
            _kernel.Bind<IUserRepository>().To<UserRepository>();
        }
    }
}