using SwGlobalData.Service.Concrete;
using SwGlobalData.Service.Interface;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace SwGlobal
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IOperatorService, OperatorService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}