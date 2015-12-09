using System;
using MovianRemote.Core;
using Autofac;

namespace MovianRemote.iOS
{
	public class iosAppSetup : AppSetup
	{

		protected override void RegisterDependencies (ContainerBuilder cb)
		{
			base.RegisterDependencies (cb);

			//cb.RegisterType<WebSocketSharpWebSocketClient> ().AsImplementedInterfaces ().SingleInstance();
		}
	}
}

