using System;
using Puzzle.NCore.Framework.Logging;
using Puzzle.NFactory.Framework.ConfigurationElements;

namespace Puzzle.NFactory.Framework
{
	public interface IContainer
	{
		ILogManager LogManager { get; set; }

		IContainerConfiguration Configuration { get; set; }

		IObjectFactory ObjectFactory { get; set; }


		object GetObject(string name, bool forceNewInstance);
		object GetObject(string name);

		object GetObjectInternal(string name, InstanceMode instanceMode);
		void ConfigureObject(object target, string configureAs);
		object CreateObject(Type objectType, params object[] args);
		object WrapInstance(object target);
#if NET2
        T Create<T>(params object[] args);
        T Get<T>(string name, bool forceNewInstance);
        T Get<T>(string name);
#endif
	}
}