using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using Puzzle.NFactory.Framework.ConfigurationElements;
using Puzzle.NFactory.Framework.TypeConverters;

namespace Puzzle.NFactory.Framework
{
	public class ConfigurationDeserializer
	{
		public IContainer Configure(XmlElement xmlRoot)
		{
			IContainer container = new Container();

			XmlElement o = xmlRoot;

            if (o == null)
                return container;

			ArrayList objectConfigurations = new ArrayList();
			ArrayList listConfigurations = new ArrayList();
			ArrayList factoryConfigurations = new ArrayList();


			foreach (XmlNode configNode in o)
			{
				if (configNode.Name == "object")
				{
					IObjectConfiguration objectConfig = ConfigureObject(configNode, container);

					objectConfigurations.Add(objectConfig);
				}

				if (configNode.Name == "list")
				{
					IListConfiguration listConfig = ConfigureList(configNode, container);

					listConfigurations.Add(listConfig);
				}

				if (configNode.Name == "factory")
				{
					IFactoryConfiguration factoryConfig = ConfigureFactory(configNode, container);

					factoryConfigurations.Add(factoryConfig);
				}

			}

			foreach (FactoryConfiguration factoryConfig in factoryConfigurations)
			{
				FillFactoryParameterTypes(factoryConfig);
				if (factoryConfig.Object != null)
					factoryConfig.Type = factoryConfig.Object.Type;

				bool isStatic = factoryConfig.Object == null;
				MethodInfo bestMethodMatch = MatchMethods(factoryConfig, factoryConfig.Type, factoryConfig.MethodName, isStatic)
					;
				if (bestMethodMatch == null)
				{
					throw new Exception(string.Format("Can not find Method for type '{0}' that matches your parameters", factoryConfig.Type));
				}
				factoryConfig.Method = bestMethodMatch;

			}


			foreach (ObjectConfiguration objectConfig in objectConfigurations)
			{
				FillCtorParameterTypes(objectConfig);

				if (objectConfig.InstanceValue is FactoryConfiguration)
				{
					FactoryConfiguration factory = objectConfig.InstanceValue as FactoryConfiguration;
					objectConfig.Type = factory.Method.ReturnType;
				}
				else
				{
					ConstructorInfo bestCtorMatch = MatchConstructors(objectConfig);
					if (bestCtorMatch == null)
					{
						throw new Exception(string.Format("Can not find CTOR for type '{0}' that matches your parameters", objectConfig.Type));
					}
					objectConfig.Constructor = bestCtorMatch;
				}

			}

			foreach (ListConfiguration listConfig in listConfigurations)
			{
				FillItemTypes(listConfig);
			}


			//cache configuration


			return container;
		}


		private IObjectConfiguration GetObjectConfiguration(string name, IContainer container)
		{
			IObjectConfiguration objectConfig = container.Configuration.GetObjectConfiguration(name);
			if (objectConfig == null)
			{
				objectConfig = new ObjectConfiguration();
				objectConfig.Name = name;
				container.Configuration.AddObjectConfiguration(objectConfig);
			}

			return objectConfig;
		}

		private IFactoryConfiguration GetFactoryConfiguration(string name, IContainer container)
		{
			IFactoryConfiguration factoryConfig = container.Configuration.GetFactoryConfiguration(name);
			if (factoryConfig == null)
			{
				factoryConfig = new FactoryConfiguration();
				factoryConfig.Name = name;
				container.Configuration.AddFactoryConfiguration(factoryConfig);
			}

			return factoryConfig;
		}

		private IListConfiguration GetListConfiguration(string name, IContainer container)
		{
			IListConfiguration listConfig = container.Configuration.GetListConfiguration(name);
			if (listConfig == null)
			{
				listConfig = new ListConfiguration();
				listConfig.Name = name;
				container.Configuration.AddListConfiguration(listConfig);
			}

			return listConfig;
		}


		private IFactoryConfiguration ConfigureFactory(XmlNode configNode, IContainer container)
		{
			string factoryName = configNode.Attributes["name"].Value;
			string factoryMethodName = configNode.Attributes["method"].Value;
			IFactoryConfiguration factoryConfig = GetFactoryConfiguration(factoryName, container);
			factoryConfig.Name = factoryName;
			factoryConfig.MethodName = factoryMethodName;


			if (configNode.Attributes["type"] != null) //
			{
				string objectTypeString = configNode.Attributes["type"].Value;
				Type objectType = ResolveType(objectTypeString);

				factoryConfig.Type = objectType;
			}
			else if (configNode.Attributes["object"] != null) //instance
			{
				string objectName = configNode.Attributes["object"].Value;
				IObjectConfiguration objectConfig = GetObjectConfiguration(objectName, container);
				factoryConfig.Object = objectConfig;
			}

			foreach (XmlNode factoryNode in configNode)
			{
				#region Parameter

				if (factoryNode.Name == "parameter")
				{
					ParameterConfiguration parameterConfig = new ParameterConfiguration();
					parameterConfig.Index = Convert.ToInt32(factoryNode.Attributes["index"].Value);


					if (factoryNode.Attributes["value"] != null)
					{
						string propertyValueString = factoryNode.Attributes["value"].Value;
						ValueConfiguration propertyValueConfig = new ValueConfiguration();
						if (factoryNode.Attributes["type"] != null)
						{
							//typed parameter
							string parameterTypeString = factoryNode.Attributes["type"].Value;
							Type parameterType = ResolveType(parameterTypeString);
							parameterConfig.Type = parameterType;
							propertyValueConfig.Value = Convert.ChangeType(propertyValueString, parameterConfig.Type);
						}
						else
						{
							//untyped parameter
							propertyValueConfig.Value = propertyValueString;
							//		parameterConfig.UntypedStringValue = propertyValueString;									
							parameterConfig.Type = null;
						}

						parameterConfig.Value = propertyValueConfig;

					}

					if (factoryNode.Attributes["object"] != null)
					{
						string parameterObjectName = factoryNode.Attributes["object"].Value;
						IObjectConfiguration propertyObjectConfig = GetObjectConfiguration(parameterObjectName, container);

						parameterConfig.Value = propertyObjectConfig;

						//done
						if (factoryNode.Attributes["instance-mode"] != null)
						{
							string instanceModeString = factoryNode.Attributes["instance-mode"].Value;
							parameterConfig.InstanceMode = (InstanceMode) InstanceMode.Parse(typeof (InstanceMode), instanceModeString);
						}
					}

					factoryConfig.ParameterConfigurations.Add(parameterConfig);
				}

				#endregion
			}

			return factoryConfig;
		}

		private void FillItemTypes(ListConfiguration listConfig)
		{
			foreach (ListItemConfiguration listItemConfig in listConfig.ListItemConfigurations)
			{
				if (listItemConfig.Value is ObjectConfiguration)
				{
					ObjectConfiguration referenceObject = listItemConfig.Value as ObjectConfiguration;
					listItemConfig.Type = referenceObject.Type;
				}
			}
		}

		private IListConfiguration ConfigureList(XmlNode configNode, IContainer container)
		{
			string listName = configNode.Attributes["name"].Value;
			IListConfiguration listConfig = GetListConfiguration(listName, container);
			listConfig.Name = listName;

			foreach (XmlNode listNode in configNode)
			{
				if (listNode.Name == "item")
				{
					ListItemConfiguration listItemConfig = new ListItemConfiguration();
					listConfig.ListItemConfigurations.Add(listItemConfig);
					ConfigureElement(listNode, listItemConfig, container, null);
				}
			}

			container.Configuration.AddListConfiguration(listConfig);
			return listConfig;
		}

		private IObjectConfiguration ConfigureObject(XmlNode configNode, IContainer container)
		{
			string objectName = configNode.Attributes["name"].Value;
			IObjectConfiguration objectConfig = GetObjectConfiguration(objectName, container);
			objectConfig.Name = objectName;


			if (configNode.Attributes["type"] != null)
			{
				string objectTypeString = configNode.Attributes["type"].Value;
				Type objectType = ResolveType(objectTypeString);
				objectConfig.Type = objectType;
			}

			if (configNode.Attributes["factory"] != null)
			{
				string factoryName = configNode.Attributes["factory"].Value;
				IFactoryConfiguration factoryConfig = GetFactoryConfiguration(factoryName, container);
				objectConfig.InstanceValue = factoryConfig;
			}


			//done
			if (configNode.Attributes["instance-mode"] != null)
			{
				string instanceModeString = configNode.Attributes["instance-mode"].Value;
				objectConfig.InstanceMode = (InstanceMode) InstanceMode.Parse(typeof (InstanceMode), instanceModeString);
			}

			container.Configuration.AddObjectConfiguration(objectConfig);

			foreach (XmlNode objectNode in configNode)
			{
				#region property

				if (objectNode.Name == "property")
				{
					PropertyConfiguration propertyConfig = new PropertyConfiguration();
					propertyConfig.Name = objectNode.Attributes["name"].Value;
					ConfigureElement(objectNode, propertyConfig, container, null);
					if (objectNode.Attributes["action"] != null)
					{
						string action = objectNode.Attributes["action"].Value;
						if (action == "Add")
							propertyConfig.ListAction = ListAction.Add;

						if (action == "Replace")
							propertyConfig.ListAction = ListAction.Replace;

					}

					objectConfig.PropertyConfigurations.Add(propertyConfig);
				}

				#endregion

				#region Ctor Parameter

				if (objectNode.Name == "ctor-parameter")
				{
					ParameterConfiguration parameterConfig = new ParameterConfiguration();
					parameterConfig.Index = Convert.ToInt32(objectNode.Attributes["index"].Value);
					ConfigureElement(objectNode, parameterConfig, container, null);
					objectConfig.CtorParameterConfigurations.Add(parameterConfig);
				}

				#endregion
			}
			return objectConfig;
		}

		private void ConfigureElement(XmlNode node, ElementConfiguration config, IContainer container, Type valueType)
		{
			config.Type = valueType;

			if (node.Attributes["value"] != null)
			{
				string propertyValueString = node.Attributes["value"].Value;

				ValueConfiguration propertyValueConfig = new ValueConfiguration();
				propertyValueConfig.Value = propertyValueString;
				config.Value = propertyValueConfig;

				if (node.Attributes["type-converter"] != null)
				{
					string typeConverterString = node.Attributes["type-converter"].Value;
					Type typeConverterType = ResolveType(typeConverterString);

					TypeConverter typeConverter = (TypeConverter) Activator.CreateInstance(typeConverterType);
					propertyValueConfig.TypeConverter = typeConverter;
				}

				if (node.Attributes["type"] != null)
				{
					string typeString = node.Attributes["type"].Value;
					Type type = ResolveType(typeString);

					config.Type = type;
				}
			}

			if (node.Attributes["object"] != null)
			{
				string propertyObjectName = node.Attributes["object"].Value;
				IObjectConfiguration propertyObjectConfig = GetObjectConfiguration(propertyObjectName, container);

				config.Value = propertyObjectConfig;

				//done
				if (node.Attributes["instance-mode"] != null)
				{
					string instanceModeString = node.Attributes["instance-mode"].Value;
					config.InstanceMode = (InstanceMode) InstanceMode.Parse(typeof (InstanceMode), instanceModeString);
				}
			}

			if (node.Attributes["list"] != null)
			{
				string propertyListName = node.Attributes["list"].Value;
				IListConfiguration propertyListConfig = GetListConfiguration(propertyListName, container);

				config.Value = propertyListConfig;
				config.Type = typeof (IList);
			}

			if (node.Attributes["factory"] != null)
			{
				string itemFactoryName = node.Attributes["factory"].Value;
				IFactoryConfiguration propertyFactoryConfig = GetFactoryConfiguration(itemFactoryName, container);

				config.Value = propertyFactoryConfig;
			}
		}

		private void FillCtorParameterTypes(ObjectConfiguration objectConfig)
		{
			foreach (ParameterConfiguration ctorParameter in objectConfig.CtorParameterConfigurations)
			{
				if (ctorParameter.Value is ObjectConfiguration)
				{
					ObjectConfiguration referenceObject = ctorParameter.Value as ObjectConfiguration;
					ctorParameter.Type = referenceObject.Type;
				}
			}
		}

		private ConstructorInfo MatchConstructors(ObjectConfiguration objectConfig)
		{
			try
			{
				ConstructorInfo[] constructors = objectConfig.Type.GetConstructors();
				ConstructorInfo bestCtorMatch = null;
				int bestScore = int.MaxValue;


				foreach (ConstructorInfo constructor in constructors)
				{
					int score = 0;
					ParameterInfo[] ctorParameters = constructor.GetParameters();

					//if param count doesnt match , move to next ctor
					if (ctorParameters.Length != objectConfig.CtorParameterConfigurations.Count)
						continue;

					if (ctorParameters.Length == 0)
						return constructor;

					#region Match parameters

					for (int i = 0; i < ctorParameters.Length; i++)
					{
						ParameterConfiguration parameterConfig = (ParameterConfiguration) objectConfig.CtorParameterConfigurations[i];
						Type type1 = ctorParameters[i].ParameterType;
						Type type2 = parameterConfig.Type;
						if (type2 == null)
						{
							ValueConfiguration parameterValueConfig = (ValueConfiguration) parameterConfig.Value;

							if (parameterValueConfig.TypeConverter != null)
							{
								if (parameterValueConfig.TypeConverter.CanConvertTo(type1))
								{
									score++;
								}
								else
								{
									break;
								}
							}
							else
							{
								//untyped parameter
								try
								{
									if (type1.IsEnum && parameterValueConfig.Value is string)
									{
										object res = Enum.Parse(type1, parameterValueConfig.Value.ToString());
									}
									else
									{
										object res = Convert.ChangeType(parameterValueConfig.Value, type1);
									}
								}
								catch
								{
									break;
								}
								score ++;
							}
						}
						else
						{
							//typed parameter

							if (type1.IsAssignableFrom(type2))
							{
								if (type1 == type2)
								{
									//same type
									score ++;
								}
								else if (type2.IsSubclassOf(type1))
								{
									//subclass
									Type tmpType = type2;
									while (tmpType != type1)
									{
										score++;
										tmpType = tmpType.BaseType;
									}
								}
								else
								{
									//interface
									score ++;
								}
							}
							else
							{
								//ignore this 
								break;
							}
						}
					}

					#endregion

					if (score < bestScore && score != 0)
					{
						bestCtorMatch = constructor;
						bestScore = score;
					}
				}
				return bestCtorMatch;
			}
			catch (Exception x)
			{
				throw new Exception(string.Format("could not match constructor for object '{0}'", objectConfig.Name), x);
			}
		}

		private MethodInfo MatchMethods(FactoryConfiguration factory, Type type, string methodName, bool isStatic)
		{
			MethodInfo[] methods = type.GetMethods();
			MethodInfo bestMethodMatch = null;
			int bestScore = int.MaxValue;


			foreach (MethodInfo method in methods)
			{
				if (method.Name != methodName)
					continue;

				int score = 0;
				ParameterInfo[] parameters = method.GetParameters();

				//if param count doesnt match , move to next ctor
				if (parameters.Length != factory.ParameterConfigurations.Count)
					continue;

				if (parameters.Length == 0)
					return method;

				#region Match parameters

				for (int i = 0; i < parameters.Length; i++)
				{
					ParameterConfiguration parameterConfig = (ParameterConfiguration) factory.ParameterConfigurations[i];
					Type type1 = parameters[i].ParameterType;
					Type type2 = parameterConfig.Type;
					if (type2 == null)
					{
						//untyped parameter
						try
						{
							ValueConfiguration parameterValueConfig = (ValueConfiguration) parameterConfig.Value;
							object res = Convert.ChangeType(parameterValueConfig.Value, type1);
						}
						catch
						{
							continue;
						}
						score ++;
					}
					else
					{
						//typed parameter

						if (type1.IsAssignableFrom(type2))
						{
							if (type1 == type2)
							{
								//same type
								score ++;
							}
							else if (type2.IsSubclassOf(type1))
							{
								//subclass
								Type tmpType = type2;
								while (tmpType != type1)
								{
									score++;
									tmpType = tmpType.BaseType;
								}
							}
							else
							{
								//interface
								score ++;
							}
						}
						else
						{
							//ignore this 
							continue;
						}
					}
				}

				#endregion

				if (score < bestScore && score != 0)
				{
					bestMethodMatch = method;
					bestScore = score;
				}
			}
			return bestMethodMatch;
		}

		private void FillFactoryParameterTypes(FactoryConfiguration factoryConfig)
		{
			foreach (ParameterConfiguration parameter in factoryConfig.ParameterConfigurations)
			{
				if (parameter.Value is ObjectConfiguration)
				{
					ObjectConfiguration referenceObject = parameter.Value as ObjectConfiguration;
					parameter.Type = referenceObject.Type;
				}
			}
		}

		private Type ResolveType(string typeString)
		{
			if (typeString == "assembly")
			{
				return typeof (AssemblyTypeConverter);
			}

			if (typeString == "webpath")
			{
				return typeof (WebPathTypeConverter);
			}

			if (typeString == "context")
			{
#if NET2
                typeString = "Puzzle.NPersist.Framework.Context, Puzzle.NPersist.Framework.NET2";
#else
                typeString = "Puzzle.NPersist.Framework.Context, Puzzle.NPersist.Framework";
#endif
            }

			Type type = Type.GetType(typeString);
			if (type == null)
				throw new Exception(string.Format("Could not resolve type '{0}'", typeString));

			return type;
		}
	}
}