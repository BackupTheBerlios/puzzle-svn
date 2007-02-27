using System;
using System.Collections;
using System.Reflection;
using Puzzle.NCore.Framework.Logging;
using Puzzle.NFactory.Framework.ConfigurationElements;
using Puzzle.NFactory.Framework.Util;

namespace Puzzle.NFactory.Framework
{
	public class Container : IContainer
	{
        public static readonly IContainer Default = ApplicationContext.Configure();

		internal Hashtable containerObjects = new Hashtable();
		internal Hashtable graphObjects = new Hashtable();
        private volatile object syncRoot = new object();

		public Container()
		{
			this.ObjectFactory = new NAspectObjectFactory();
			this.LogManager = new LogManager();
			this.Configuration = new ContainerConfiguration();
        }

        #region Public Property ParentContainer
        private IContainer parentContainer;
        public virtual IContainer ParentContainer
        {
            get
            {
                return this.parentContainer;
            }
            set
            {
                this.parentContainer = value;
            }
        }
        #endregion

		#region Public Property LogManager

		private ILogManager logManager;

		public ILogManager LogManager
		{
			get { return this.logManager; }
			set { this.logManager = value; }
		}

		#endregion

		#region Public Property Configuration

		private IContainerConfiguration configuration;

		public IContainerConfiguration Configuration
		{
			get { return this.configuration; }
			set { this.configuration = value; }
		}

		#endregion

		#region Public Property ObjectFactory

		private IObjectFactory objectFactory;

		public IObjectFactory ObjectFactory
		{
			get { return this.objectFactory; }
			set { this.objectFactory = value; }
		}

		#endregion


        public object GetObject(string name)
        {
            return GetObject(name, false);
        }

        public void PrepareNewGraph()
        {
            graphObjects.Clear();
            if (ParentContainer != null)
                ParentContainer.PrepareNewGraph();
        }

		public object GetObject(string name, bool forceNewInstance)
		{
            lock (syncRoot)
            {
                //clear the graph cache
                PrepareNewGraph();

                LogMessage message = new LogMessage("Getting object '{0}'", name);
                LogMessage verbose = new LogMessage("Force new instance {0}", forceNewInstance);
                LogManager.Info(this, message, verbose);
                return GetObjectInternal(name, forceNewInstance ? InstanceMode.PerReference : InstanceMode.Default);
            }
		}

#if NET2
        public T GetObject<T>(string name)
        {
            object o = GetObject(name, false);
            ValidateType<T>(name, o);
            return (T)o;
        }

        private static void ValidateType<T>(string name, object o)
        {
            Type type = typeof(T);
            if (!(type.IsAssignableFrom(o.GetType())))
                throw new Exception(string.Format("Cannot cast object '{0}' to type '{1}'", name, type.Name));
        }

        public T GetObject<T>(string name, bool forceNewInstance)
		{
            object o = GetObject(name, forceNewInstance);
            ValidateType<T>(name, o);
            return (T)o;
		}

        public T CreateObject<T>(params object[] args)
        {
            return (T)CreateObject(typeof(T), args);
        }
#endif


        public object GetObjectInternal(string name, InstanceMode instanceMode)
		{
            lock (syncRoot)
            {
                IObjectConfiguration objectConfig = Configuration.GetObjectConfiguration(name);

                if (objectConfig == null)
                {
                    if (ParentContainer == null)
                        throw new Exception(string.Format("Object not found '{0}'", name));
                    else
                        return ParentContainer.GetObjectInternal(name, instanceMode);
                }

                if (instanceMode == InstanceMode.Default)
                    instanceMode = objectConfig.InstanceMode;

                if (instanceMode == InstanceMode.Default)
                    instanceMode = InstanceMode.PerContainer;

                //fetch object from container cache
                if (instanceMode == InstanceMode.PerContainer)
                {
                    //only fetch from cache if we dont need a new instance
                    if (containerObjects[name] != null)
                        return containerObjects[name];
                }

                //fetch object from graph cache
                if (instanceMode == InstanceMode.PerGraph)
                {
                    //only fetch from cache if we dont need a new instance
                    if (graphObjects[name] != null)
                        return graphObjects[name];
                }

                object instance = CreateInstance(objectConfig);

                //store instance in container cache
                if (instanceMode == InstanceMode.PerContainer)
                {
                    containerObjects[objectConfig.Name] = instance;
                }

                //store instance in graph cache
                if (instanceMode == InstanceMode.PerGraph)
                {
                    graphObjects[objectConfig.Name] = instance;
                }

                ConfigureObject(objectConfig, instance);

                return instance;
            }
		}

		private object CreateInstance(IObjectConfiguration objectConfig)
		{
			if (objectConfig.InstanceValue is IFactoryConfiguration)
			{
				IFactoryConfiguration factoryConfig = objectConfig.InstanceValue as IFactoryConfiguration;
				return factoryConfig.Invoke(this, null, InstanceMode.Default);
			}
			else
			{
				return objectConfig.CreateObject(this);
			}
		}

		private void ConfigureObject(IObjectConfiguration objectConfig, object target)
		{
			ArrayList sortedPropertyConfigurations = new ArrayList(objectConfig.PropertyConfigurations);
			sortedPropertyConfigurations.Sort(new PropertyPathSorter());

			LogMessage message = new LogMessage("Configuring object '{0}' as '{1}'", target, objectConfig.Name);			
			LogManager.Info(this, message);

			foreach (PropertyConfiguration propertyConfig in sortedPropertyConfigurations)
			{
				string propertyPath = propertyConfig.Name;
				string[] properties = propertyPath.Split('.');

				object tmpTarget = target;
				PropertyInfo propertyInfo = null;
				//traverse property path
				for (int i = 0; i < properties.Length; i++)
				{
					if (i > 0)
					{
						tmpTarget = propertyInfo.GetValue(tmpTarget, null);
					}

					string property = properties[i];
					Type tmpType = tmpTarget.GetType();
					propertyInfo = tmpType.GetProperty(property);

                    if (propertyInfo == null)
                    {
                        throw new NullReferenceException(string.Format("Property path {0} was not found", propertyPath));
                    }
				}

                

				target = tmpTarget;
				propertyConfig.Type = propertyInfo.PropertyType;

				object res = propertyConfig.GetValue(this);
				if (propertyConfig.Value is IListConfiguration)
				{
					IList orgList = (IList) propertyInfo.GetValue(target, null);

					if (orgList != null)
					{
						IList resList = (IList) res;

						if (propertyConfig.ListAction == ListAction.Replace)
						{
							orgList.Clear();
						}

						foreach (object item in resList)
						{
							orgList.Add(item);
						}
					}
					else
					{
						propertyInfo.SetValue(target, res, null);
					}


				}
				else
				{
					if (propertyConfig.ListAction == ListAction.Add)
					{
						IList orgList = (IList) propertyInfo.GetValue(target, null);
						orgList.Add(res);
					}
					else
					{
						propertyInfo.SetValue(target, res, null);
					}
				}
			}
		}

		

		public void ConfigureObject(object target, string configureAs)
		{
			ConfigureObject(Configuration.GetObjectConfiguration(configureAs), target);
		}

		public object CreateObject(Type objectType, params object[] args)
		{
			return ObjectFactory.CreateInstance(null, objectType, args);
		}

		public object WrapInstance(object target)
		{
			return null;
		}

	}
}