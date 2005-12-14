using System;
using System.Collections;
using System.Reflection;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public class FactoryConfiguration : IFactoryConfiguration
	{
		#region Public Property ParameterConfigurations

		private ArrayList parameterConfigurations = new ArrayList();

		public ArrayList ParameterConfigurations
		{
			get { return this.parameterConfigurations; }
		}

		#endregion

		#region Public Property Name

		private string name;

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		#endregion

		#region Public Property MethodName

		private string methodName;

		public string MethodName
		{
			get { return this.methodName; }
			set { this.methodName = value; }
		}

		#endregion

		#region Public Property Method

		private MethodInfo method;

		public MethodInfo Method
		{
			get { return this.method; }
			set { this.method = value; }
		}

		#endregion

		#region Public Property Type

		private Type type;

		public Type Type
		{
			get { return this.type; }
			set { this.type = value; }
		}

		#endregion

		#region Public Property Object

		private IObjectConfiguration _object;

		public IObjectConfiguration Object
		{
			get { return this._object; }
			set { this._object = value; }
		}

		#endregion

		public object Invoke(IContainer owner, Type requestedType, InstanceMode instanceMode)
		{
			object[] parameters = new object[ParameterConfigurations.Count];
			ParameterInfo[] parameterInfos = method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterConfiguration paramConfig = (ParameterConfiguration) ParameterConfigurations[i];
				paramConfig.Type = parameterInfos[i].ParameterType;
				parameters[i] = paramConfig.GetValue(owner);
			}


			object target = null;
			if (Object != null)
				target = owner.GetObject(Object.Name);

			object res = method.Invoke(target, parameters);

			return res;
		}
	}
}