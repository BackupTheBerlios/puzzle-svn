// *
// * Copyright (C) 2005 Roger Johansson : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using Puzzle.NAspect.Framework.Aop;

namespace Puzzle.NAspect.Framework
{
	public class InterfaceProxyFactory
	{
		public static Type CreateProxyType(Type baseType, IList aspects, IList mixins, Engine engine)
		{
			if (aspects.Count == 0 && mixins.Count == 1)
				return baseType;

			InterfaceProxyFactory factory = new InterfaceProxyFactory(engine);

			return factory.CreateType(baseType, aspects, mixins);
		}

		private static long guid = 0;

		private static string GetMethodId(string methodName)
		{
			string methodId = "__" + methodName + "Wrapper" + guid.ToString();
			guid++;

			if (guid == long.MaxValue)
				guid = long.MinValue;

			if (guid == 0)
				throw new Exception("tough luch , youve proxied long.max methods");

			return methodId;
		}

		private Engine engine;
		private ArrayList wrapperMethods = new ArrayList();

		public InterfaceProxyFactory(Engine engine)
		{
			this.engine = engine;
		}


		private AssemblyBuilder GetAssemblyBuilder()
		{
			AppDomain domain = Thread.GetDomain();
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = Guid.NewGuid().ToString();
			assemblyName.Version = new Version(1, 0, 0, 0);
			AssemblyBuilder assemblyBuilder = domain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
			return assemblyBuilder;
		}

		public Type CreateType(Type baseType, IList aspects, IList mixins)
		{
			string typeName = baseType.Name + "AopWrapper";
			string moduleName = "Puzzle.NAspect.Runtime.Proxy";

			AssemblyBuilder assemblyBuilder = GetAssemblyBuilder();

			Type[] interfaces = GetInterfaces(baseType, mixins);
			Type[] mixinInterfaces = GetMixinInterfaces(baseType, mixins);


			TypeBuilder typeBuilder = GetTypeBuilder(assemblyBuilder, moduleName, typeName, typeof(InterfaceProxyBase), interfaces);
			
			BuildWrappedInstanceField(typeBuilder, baseType);
			BuildMixinFields(typeBuilder, mixins);

			BuildConstructors(baseType, typeBuilder, mixins);

			foreach (Type mixinType in mixins)
			{
				if (mixinType.IsInterface)
				{
					//ignore pure interfaces
				}
				else
				{
					Type mixinInterfaceType = mixinType.GetInterfaces() [0];
					MixinType(typeBuilder, mixinInterfaceType, GetMixinField(mixinInterfaceType), aspects);
				}
			}

			BuildMethods(baseType,baseType.GetInterfaces(), typeBuilder, mixins, aspects);

			Type proxyType = typeBuilder.CreateType();

			BuildLookupTables(baseType, proxyType, aspects, mixins);

			return proxyType;
		}

		FieldBuilder that;
		private void BuildWrappedInstanceField(TypeBuilder typeBuilder, Type basetype)
		{
			that = typeBuilder.DefineField("__wrappedInstanceField", basetype, FieldAttributes.Private);
		}

		private void BuildLookupTables(Type baseType, Type proxyType, IList aspects, IList mixins)
		{
			foreach (string methodId in wrapperMethods)
			{
				MethodInfo wrapperMethod = proxyType.GetMethod(methodId, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
				MethodCache.wrapperMethodLookup[methodId] = wrapperMethod;

				MethodBase baseMethod = (MethodBase) MethodCache.methodLookup[methodId];
				//array to return
				IList methodinterceptors = new ArrayList();
				//fetch all aspects from the type-aspect lookup
				foreach (IAspect aspect in aspects)
				{
					foreach (IPointcut pointcut in aspect.Pointcuts)
					{
						if (pointcut.IsMatch(baseMethod))
						{
							foreach (IInterceptor interceptor in pointcut.Interceptors)
							{
								methodinterceptors.Add(interceptor);
							}
						}
					}
				}

				MethodCache.methodInterceptorsLookup[methodId] = methodinterceptors;
			}
		}

		private void BuildMethods(Type baseType,Type[] interfaces, TypeBuilder typeBuilder, IList mixins, IList aspects)
		{
		//	MethodInfo[] methods = baseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

			foreach (Type iface in interfaces)
			{
				MethodInfo[] methods = iface.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
				foreach (MethodInfo method in methods)
				{
				
					string methodName = iface.FullName.ToString()+"."+method.Name;

					if (engine.PointCutMatcher.MethodShouldBeProxied(method, aspects))
					{
						BuildMethod(methodName,baseType, typeBuilder, method, mixins);
					}	
					else
					{
						BuildUnproxiedMethod (methodName,baseType,typeBuilder,method,mixins);
					}
				}
			}
		}


		private void BuildMethod(string methodName,Type baseType, TypeBuilder typeBuilder, MethodInfo method, IList mixins)
		{
			string wrapperName = GetMethodId(method.Name);
			wrapperMethods.Add(wrapperName);

			MethodCache.methodLookup[wrapperName] = method;

			ParameterInfo[] parameterInfos = method.GetParameters();
			Type[] parameterTypes = new Type[parameterInfos.Length];
			for (int i = 0; i < parameterInfos.Length; i++)
				parameterTypes[i] = parameterInfos[i].ParameterType;		

			
			MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodName, MethodAttributes.NewSlot | MethodAttributes.Private |  MethodAttributes.Virtual |  MethodAttributes.Final | MethodAttributes.HideBySig , CallingConventions.Standard, method.ReturnType, parameterTypes);
			for (int i = 0; i < parameterInfos.Length; i++)
			{
				ParameterBuilder parameterBuilder = methodBuilder.DefineParameter(i + 1, parameterInfos[i].Attributes, parameterInfos[i].Name);
			}
			typeBuilder.DefineMethodOverride(methodBuilder,method);

			ILGenerator il = methodBuilder.GetILGenerator();


			LocalBuilder paramList = il.DeclareLocal(typeof (ArrayList));


			//create param arraylist
			ConstructorInfo arrayListCtor = typeof (ArrayList).GetConstructor(new Type[0]);
			il.Emit(OpCodes.Newobj, arrayListCtor);
			il.Emit(OpCodes.Stloc, paramList);


			int j = 0;
			ConstructorInfo interceptedParameterCtor = typeof (InterceptedParameter).GetConstructors()[0];
			MethodInfo arrayListAddMethod = typeof (ArrayList).GetMethod("Add");
			MethodInfo getTypeMethod = typeof (Type).GetMethod("GetType", new Type[1] {typeof (string)});

			foreach (ParameterInfo parameter in parameterInfos)
			{
				il.Emit(OpCodes.Ldloc, paramList);
				string paramName = parameter.Name;
				if (paramName == null)
				{
					paramName = "param" + j.ToString();
				}
				il.Emit(OpCodes.Ldstr, paramName);
				il.Emit(OpCodes.Ldc_I4, j);
				il.Emit(OpCodes.Ldstr, parameter.ParameterType.FullName.Replace("&", ""));
				il.Emit(OpCodes.Call, getTypeMethod);


				il.Emit(OpCodes.Ldarg, j + 1);

				if (parameter.ParameterType.FullName.IndexOf("&") >= 0)
				{
					il.Emit(OpCodes.Ldind_Ref);
					Type t = Type.GetType(parameter.ParameterType.FullName.Replace("&", ""));
					if (t.IsValueType)
						il.Emit(OpCodes.Box, t);
				}
				if (parameter.ParameterType.IsValueType)
				{
					il.Emit(OpCodes.Box, parameter.ParameterType);
				}
				il.Emit(OpCodes.Ldc_I4, (int) ParameterType.ByVal);
				il.Emit(OpCodes.Newobj, interceptedParameterCtor);
				il.Emit(OpCodes.Callvirt, arrayListAddMethod);
				il.Emit(OpCodes.Pop);


				j++;
			}

			MethodInfo handleCallMethod = typeof (IAopProxy).GetMethod("HandleCall");

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldstr, wrapperName);
			il.Emit(OpCodes.Ldloc, paramList);
			il.Emit(OpCodes.Ldstr, method.ReturnType.FullName);
			il.Emit(OpCodes.Call, getTypeMethod);
			il.Emit(OpCodes.Callvirt, handleCallMethod);
			if (method.ReturnType == typeof (void))
			{
				il.Emit(OpCodes.Pop);
			}
			else if (method.ReturnType.IsValueType)
			{
				il.Emit(OpCodes.Unbox, method.ReturnType);
				il.Emit(OpCodes.Ldobj, method.ReturnType);
			}


			j = 0;
			MethodInfo get_ItemMethod = typeof (ArrayList).GetMethod("get_Item", new Type[1] {typeof (int)});
			foreach (ParameterInfo parameter in parameterInfos)
			{
				if (parameter.ParameterType.FullName.IndexOf("&") >= 0)
				{
					il.Emit(OpCodes.Ldarg, j + 1);
					il.Emit(OpCodes.Ldloc, paramList);
					il.Emit(OpCodes.Ldc_I4, j);
					il.Emit(OpCodes.Callvirt, get_ItemMethod);
					il.Emit(OpCodes.Castclass, typeof (InterceptedParameter));
					FieldInfo valueField = typeof (InterceptedParameter).GetField("Value");
					il.Emit(OpCodes.Ldfld, valueField);
					Type t = Type.GetType(parameter.ParameterType.FullName.Replace("&", ""));
					if (t.IsValueType)
					{
						il.Emit(OpCodes.Unbox, t);
						il.Emit(OpCodes.Ldobj, t);
						il.Emit(OpCodes.Stobj, t);
					}
					else
					{
						il.Emit(OpCodes.Castclass, t);
						il.Emit(OpCodes.Stind_Ref);
					}


				}
				j++;
			}


			il.Emit(OpCodes.Ret);

			BuildWrapperMethod(wrapperName, baseType, typeBuilder, method, mixins);
		}

		private void BuildUnproxiedMethod(string wrapperName, Type baseType, TypeBuilder typeBuilder, MethodInfo method, IList mixins)
		{
			wrapperName = method.Name;

			ParameterInfo[] parameterInfos = method.GetParameters();
			Type[] parameterTypes = new Type[parameterInfos.Length];
			for (int i = 0; i < parameterInfos.Length; i++)
				parameterTypes[i] = parameterInfos[i].ParameterType;

			Type returnType = method.ReturnType;

			MethodBuilder methodBuilder = typeBuilder.DefineMethod(wrapperName,MethodAttributes.NewSlot | MethodAttributes.Private |  MethodAttributes.Virtual |  MethodAttributes.Final | MethodAttributes.HideBySig , CallingConventions.Standard, returnType, parameterTypes);
			typeBuilder.DefineMethodOverride(methodBuilder,method);

			for (int i = 0; i < parameterInfos.Length; i++)
			{
				methodBuilder.DefineParameter(i + 1, parameterInfos[i].Attributes, parameterInfos[i].Name);
			}

			ILGenerator il = methodBuilder.GetILGenerator();
			
			//			il.EmitWriteLine("enter "  + wrapperName) ;

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld,that);
			for (int i = 0; i < parameterInfos.Length; i++)
				il.Emit(OpCodes.Ldarg, i + 1);
	
			il.Emit(OpCodes.Callvirt, method);
	
			il.Emit(OpCodes.Ret);
			
		}

		private void BuildWrapperMethod(string wrapperName, Type baseType, TypeBuilder typeBuilder, MethodInfo method, IList mixins)
		{

			ParameterInfo[] parameterInfos = method.GetParameters();
			Type[] parameterTypes = new Type[parameterInfos.Length];
			for (int i = 0; i < parameterInfos.Length; i++)
				parameterTypes[i] = parameterInfos[i].ParameterType;

			Type returnType = method.ReturnType;

			MethodBuilder methodBuilder = typeBuilder.DefineMethod(wrapperName,MethodAttributes.NewSlot | MethodAttributes.Private |  MethodAttributes.Virtual |  MethodAttributes.Final | MethodAttributes.HideBySig , CallingConventions.Standard, returnType, parameterTypes);

			ILGenerator il = methodBuilder.GetILGenerator();
			//			il.EmitWriteLine("enter "  + wrapperName) ;

			il.Emit(OpCodes.Ldarg_0) ;
			il.Emit(OpCodes.Ldfld,that);
			for (int i = 0; i < parameterInfos.Length; i++)
				il.Emit(OpCodes.Ldarg, i + 1);
	
			il.Emit(OpCodes.Callvirt, method);
	
			il.Emit(OpCodes.Ret);
			
		}

		private FieldBuilder GetMixinField(Type mixinType)
		{
			if (!mixinType.IsInterface)
				mixinType = mixinType.GetInterfaces()[0];

			string mixinName = mixinType.Name + "Mixin";

			return mixinFieldLookup[mixinName] as FieldBuilder;
		}

		private Hashtable mixinFieldLookup = new Hashtable();

		private void BuildMixinFields(TypeBuilder typeBuilder, IList mixins)
		{
			foreach (Type mixinType in mixins)
			{
				if (mixinType.IsInterface)
				{
				}
				else
				{
					Type mixinInterface = mixinType.GetInterfaces() [0];

					string mixinName = mixinInterface.Name + "Mixin";
					FieldBuilder mixinField = typeBuilder.DefineField(mixinName, mixinInterface, FieldAttributes.Private);
					mixinFieldLookup[mixinName] = mixinField;
				}
			}
		}

		private static Type[] GetInterfaces(Type baseType, IList mixins)
		{
			Type[] mixinInterfaces = GetMixinInterfaces(baseType,mixins);
			Type[] baseInterfaces =  baseType.GetInterfaces();

			Type[] interfaces = new Type[mixinInterfaces.Length+baseInterfaces.Length] ;
			Array.Copy(mixinInterfaces,0,interfaces,0,mixinInterfaces.Length) ;
			Array.Copy(baseInterfaces,0,interfaces,mixinInterfaces.Length,baseInterfaces.Length) ;

			return interfaces;
		}

		private static Type[] GetMixinInterfaces(Type baseType, IList mixins)
		{
			Type[] mixinInterfaces = new Type[mixins.Count];
			for (int i = 0; i < mixins.Count; i++)
			{
				Type mixin = mixins[i] as Type;
				if (mixin.IsInterface)
				{
					mixinInterfaces[i] = mixin;
				}
				else
				{
					Type mixinInterface = mixin.GetInterfaces()[0];
					mixinInterfaces[i] = mixinInterface;
				}
			}

			return mixinInterfaces;
		}

		private static TypeBuilder GetTypeBuilder(AssemblyBuilder assemblyBuilder, string moduleName, string typeName, Type baseType, Type[] interfaces)
		{
			ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(moduleName);
			TypeAttributes typeAttributes = TypeAttributes.Class | TypeAttributes.Public;
			return moduleBuilder.DefineType(typeName, typeAttributes, baseType, interfaces);
		}

		public void MixinType(TypeBuilder typeBuilder, Type mixinInterfaceType, FieldBuilder mixinField, IList aspects)
		{
			MethodInfo[] methods = mixinInterfaceType.GetMethods();


			BuildMixinMethods(methods, typeBuilder, mixinField, aspects);

			Type[] inheritedInterfaces = mixinInterfaceType.GetInterfaces();
			foreach (Type inheritedInterface in  inheritedInterfaces)
			{
				MixinType(typeBuilder, inheritedInterface, mixinField, aspects);
			}
		}

		private void BuildMixinMethods(MethodInfo[] methods, TypeBuilder typeBuilder, FieldBuilder mixinField, IList aspects)
		{
			foreach (MethodInfo method in methods)
			{
//				if (method.IsVirtual && !method.IsFinal && engine.PointCutMatcher.MethodShouldBeProxied(method, aspects))
//				{
//					BuildMixinMethod(typeBuilder, method, mixinField);
//				}
//				else
//				{
					BuildMixinUnproxiedMethod(method.Name, typeBuilder, method, mixinField);
//				}
			}
		}

		private void BuildMixinMethod(TypeBuilder typeBuilder, MethodInfo method, FieldBuilder field)
		{
			if (method.DeclaringType == typeof (IAopProxy))
			{
				BuildMixinWrapperMethod(method.Name, typeBuilder, method, field);
				return;
			}


			string wrapperName = GetMethodId(method.Name);
			wrapperMethods.Add(wrapperName);
			MethodCache.methodLookup[wrapperName] = method;
			//		MethodCache.wrapperMethodLookup[wrapperName] = method;

			ParameterInfo[] parameterInfos = method.GetParameters();
			Type[] parameterTypes = new Type[parameterInfos.Length];
			for (int i = 0; i < parameterInfos.Length; i++)
				parameterTypes[i] = parameterInfos[i].ParameterType;

			MethodBuilder methodBuilder = typeBuilder.DefineMethod(method.Name, MethodAttributes.Public | MethodAttributes.Virtual, CallingConventions.Standard, method.ReturnType, parameterTypes);
			for (int i = 0; i < parameterInfos.Length; i++)
			{
				ParameterBuilder parameterBuilder = methodBuilder.DefineParameter(i + 1, parameterInfos[i].Attributes, parameterInfos[i].Name);
			}

			ILGenerator il = methodBuilder.GetILGenerator();

			LocalBuilder paramList = il.DeclareLocal(typeof (ArrayList));


			//create param arraylist
			ConstructorInfo arrayListCtor = typeof (ArrayList).GetConstructor(new Type[0]);
			il.Emit(OpCodes.Newobj, arrayListCtor);
			il.Emit(OpCodes.Stloc, paramList);


			int j = 0;
			ConstructorInfo interceptedParameterCtor = typeof (InterceptedParameter).GetConstructors()[0];
			MethodInfo arrayListAddMethod = typeof (ArrayList).GetMethod("Add");
			MethodInfo getTypeMethod = typeof (Type).GetMethod("GetType", new Type[1] {typeof (string)});

			foreach (ParameterInfo parameter in parameterInfos)
			{
				il.Emit(OpCodes.Ldloc, paramList);
				string paramName = parameter.Name;
				if (paramName == null)
				{
					paramName = "param" + j.ToString();
				}
				il.Emit(OpCodes.Ldstr, paramName);
				il.Emit(OpCodes.Ldc_I4, j);
				il.Emit(OpCodes.Ldstr, parameter.ParameterType.FullName.Replace("&", ""));
				il.Emit(OpCodes.Call, getTypeMethod);


				il.Emit(OpCodes.Ldarg, j + 1);

				if (parameter.ParameterType.FullName.IndexOf("&") >= 0)
				{
					il.Emit(OpCodes.Ldind_Ref);
					Type t = Type.GetType(parameter.ParameterType.FullName.Replace("&", ""));
					if (t.IsValueType)
						il.Emit(OpCodes.Box, t);
				}
				if (parameter.ParameterType.IsValueType)
				{
					il.Emit(OpCodes.Box, parameter.ParameterType);
				}
				il.Emit(OpCodes.Ldc_I4, (int) ParameterType.ByVal);
				il.Emit(OpCodes.Newobj, interceptedParameterCtor);
				il.Emit(OpCodes.Callvirt, arrayListAddMethod);
				il.Emit(OpCodes.Pop);


				j++;
			}

			MethodInfo handleCallMethod = typeof (IAopProxy).GetMethod("HandleCall");

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldstr, wrapperName);
			il.Emit(OpCodes.Ldloc, paramList);
			il.Emit(OpCodes.Ldstr, method.ReturnType.FullName);
			il.Emit(OpCodes.Call, getTypeMethod);
			il.Emit(OpCodes.Callvirt, handleCallMethod);
			if (method.ReturnType == typeof (void))
			{
				il.Emit(OpCodes.Pop);
			}
			else if (method.ReturnType.IsValueType)
			{
				il.Emit(OpCodes.Unbox, method.ReturnType);
				il.Emit(OpCodes.Ldobj, method.ReturnType);
			}


			j = 0;
			MethodInfo get_ItemMethod = typeof (ArrayList).GetMethod("get_Item", new Type[1] {typeof (int)});
			foreach (ParameterInfo parameter in parameterInfos)
			{
				if (parameter.ParameterType.FullName.IndexOf("&") >= 0)
				{
					il.Emit(OpCodes.Ldarg, j + 1);
					il.Emit(OpCodes.Ldloc, paramList);
					il.Emit(OpCodes.Ldc_I4, j);
					il.Emit(OpCodes.Callvirt, get_ItemMethod);
					il.Emit(OpCodes.Castclass, typeof (InterceptedParameter));
					FieldInfo valueField = typeof (InterceptedParameter).GetField("Value");
					il.Emit(OpCodes.Ldfld, valueField);
					Type t = Type.GetType(parameter.ParameterType.FullName.Replace("&", ""));
					if (t.IsValueType)
					{
						il.Emit(OpCodes.Unbox, t);
						il.Emit(OpCodes.Ldobj, t);
						il.Emit(OpCodes.Stobj, t);
					}
					else
					{
						il.Emit(OpCodes.Castclass, t);
						il.Emit(OpCodes.Stind_Ref);
					}


				}
				j++;
			}


			il.Emit(OpCodes.Ret);


			BuildMixinWrapperMethod(wrapperName, typeBuilder, method, field);
		}

		private void BuildMixinUnproxiedMethod(string wrapperName, TypeBuilder typeBuilder, MethodInfo method, FieldBuilder field)
		{
			BuildMixinWrapperMethod(wrapperName,typeBuilder,method,field);
		}

		private void BuildMixinWrapperMethod(string wrapperName, TypeBuilder typeBuilder, MethodInfo method, FieldBuilder field)
		{
			ParameterInfo[] parameterInfos = method.GetParameters();
			Type[] parameterTypes = new Type[parameterInfos.Length];
			for (int i = 0; i < parameterInfos.Length; i++)
				parameterTypes[i] = parameterInfos[i].ParameterType;

			MethodBuilder methodBuilder = typeBuilder.DefineMethod(wrapperName, MethodAttributes.Public | MethodAttributes.Virtual, CallingConventions.Standard, method.ReturnType, parameterTypes);

			for (int i = 0; i < parameterInfos.Length; i++)
			{
				methodBuilder.DefineParameter(i + 1, parameterInfos[i].Attributes, parameterInfos[i].Name);
			}

			ILGenerator il = methodBuilder.GetILGenerator();

			il.EmitWriteLine("hej hopp") ;

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldfld, field);
			for (int i = 0; i < parameterInfos.Length; i++)
				il.Emit(OpCodes.Ldarg, i + 1);

			il.Emit(OpCodes.Callvirt, method);
			//	il.EmitWriteLine(method.Name) ;
			il.Emit(OpCodes.Ret);
		}

		public void BuildConstructors(Type baseType,TypeBuilder typeBuilder, IList mixins)
		{
			ConstructorInfo constructor = typeof(object).GetConstructor(new Type[]{});
			BuildConstructor(baseType, constructor, typeBuilder, mixins);
		}

		private void BuildConstructor(Type baseType, ConstructorInfo constructor, TypeBuilder typeBuilder, IList mixins)
		{
			//make proxy ctor param count same as superclass
			Type[] parameterTypes = new Type[1];
			
			parameterTypes[0] = baseType;


			ConstructorBuilder proxyConstructor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, parameterTypes);
			ILGenerator il = proxyConstructor.GetILGenerator();

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Call,constructor) ;

			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Stfld, that);
			 

			foreach (Type mixinType in mixins)
			{
				if (mixinType.IsInterface)
				{
					//ignore interface type mixins , they do not have an impelemntation
				}
				else
				{
					//				il.EmitWriteLine("setting mixin instance " + mixinType.FullName) ;
					il.Emit(OpCodes.Ldarg_0);
					ConstructorInfo mixinCtor = (mixinType).GetConstructor(new Type[] {});
					il.Emit(OpCodes.Newobj, mixinCtor);
					il.Emit(OpCodes.Stfld, GetMixinField(mixinType));
				}
			}

			//associate iproxyaware mixins with this instance
			MethodInfo setProxyMethod = typeof (IProxyAware).GetMethod("SetProxy");
			foreach (Type mixinType in mixins)
			{
				if (mixinType.IsInterface)
				{
					//ignore interface type mixins , they do not have an impelemntation
				}
				else
				{
					if (typeof (IProxyAware).IsAssignableFrom(mixinType))
					{
						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Ldfld, GetMixinField(mixinType));
						il.Emit(OpCodes.Ldarg_0);
						il.Emit(OpCodes.Callvirt, setProxyMethod);
					}
				}
			}
			il.Emit(OpCodes.Ret);
		}
	}
}