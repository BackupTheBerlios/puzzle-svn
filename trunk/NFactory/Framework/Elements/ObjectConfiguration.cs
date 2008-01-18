using System;
using System.Collections;
using System.Reflection;
using Puzzle.NCore.Framework.Logging;
using Puzzle.NAspect.Framework;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public class ObjectConfiguration : IObjectConfiguration
	{
		#region Public Property PropertyConfigurations

		private ArrayList propertyConfigurations = new ArrayList();

		public ArrayList PropertyConfigurations
		{
			get { return this.propertyConfigurations; }
		}

		#endregion

		#region Public Property CtorParameterConfigurations

		private ArrayList ctorParameterConfigurations = new ArrayList();

		public ArrayList CtorParameterConfigurations
		{
			get { return this.ctorParameterConfigurations; }
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

		#region Public Property Type

		private Type type;

		public Type Type
		{
			get { return this.type; }
			set { this.type = value; }
		}

		#endregion

		#region Public Property Constructor

		private ConstructorInfo constructor;

		public ConstructorInfo Constructor
		{
			get { return this.constructor; }
			set { this.constructor = value; }
		}

		#endregion

		#region Public Property InstanceMode

		private InstanceMode instanceMode;

		public InstanceMode InstanceMode
		{
			get { return this.instanceMode; }
			set { this.instanceMode = value; }
		}

		#endregion

		#region Public Property InstanceValue

		private IValue instanceValue;

		public IValue InstanceValue
		{
			get { return this.instanceValue; }
			set { this.instanceValue = value; }
		}

		#endregion

        #region Public Property AopEngine 
        private IEngine aopEngine;
        public IEngine AopEngine
        {
            get
            {
                return this.aopEngine;
            }
            set
            {
                this.aopEngine = value;
            }
        }                        
        #endregion

		public object Invoke(IContainer owner, Type requestedType, InstanceMode instanceMode)
		{
			return owner.GetObjectInternal(this.Name, instanceMode,null);
		}

		public object CreateObject(IContainer owner)
		{
			object[] ctorParams = new object[this.CtorParameterConfigurations.Count];
			ParameterInfo[] ctorParameterInfos = this.Constructor.GetParameters();
			for (int i = 0; i < ctorParams.Length; i++)
			{
				ParameterConfiguration paramConfig = (ParameterConfiguration) this.CtorParameterConfigurations[i];
				paramConfig.Type = ctorParameterInfos[i].ParameterType;

				object res = paramConfig.GetValue(owner);
				ctorParams[i] = res;

			}
			object instance = null;
			try
			{
				instance = owner.ObjectFactory.CreateInstance(aopEngine , this.Type, ctorParams);
				LogMessage message = new LogMessage("Creating instance of type '{0}' from config '{1}'", this.Type, this.Name);
				owner.LogManager.Info(this, message);
			}
			catch 
			{
				LogMessage message = new LogMessage("Failed to create instance of type '{0}' from config '{1}'", this.Type, this.Name);
				owner.LogManager.Fatal(this, message);

				throw;
			}


			return instance;
		}
	}

	public enum InstanceMode
	{
		Default,
		PerContainer,
		PerGraph,
		PerReference,
	}
}