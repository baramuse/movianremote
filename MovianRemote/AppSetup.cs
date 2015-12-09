using System;
using Autofac;
using System.Reflection;
using MovianRemote.Core.Interfaces;
using MovianRemote.Core.Implementations;

namespace MovianRemote.Core
{
	public class AppSetup  
	{
		public ContainerBuilder CreateContainerBuilder() 
		{
			var containerBuilder = new ContainerBuilder();
			RegisterDependencies(containerBuilder);
			return containerBuilder;
		}

		protected virtual void RegisterDependencies(ContainerBuilder cb)
		{
			var a = typeof(AppSetup).GetTypeInfo ().Assembly;

			cb.RegisterType<App> ().As<IAppNavigation> ();
			//cb.RegisterType<MovianRemote.Core.Services.MovianWebSocketService> ().AsImplementedInterfaces ().SingleInstance();
			cb.RegisterType<WebSocketPortableWebSocketClient> ().AsImplementedInterfaces ().SingleInstance ();
			cb.RegisterAssemblyTypes (a)
				.Where (t => t.Name.EndsWith ("Service"))
				.AsImplementedInterfaces ()
				.SingleInstance ();
			cb.RegisterAssemblyTypes (a)
				.Where (t => t.Name.EndsWith ("ViewModel"));
		}
	}
}

