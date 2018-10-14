using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LampStore.Domain.Abstract;
using LampStore.Domain.Concrete;

namespace LampStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
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
            _kernel.Bind<IProductRepository>().To<EfProductRepository>();
            _kernel.Bind<IUserRepository>().To<EfUserRepository>();
            _kernel.Bind<IOrderRepository>().To<EfOrderRepository>();
        }
    }
}