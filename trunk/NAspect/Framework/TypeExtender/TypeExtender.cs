using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using System.Threading;

namespace Puzzle.NAspect.Framework
{
	public class TypeExtender : ITypeExtender
	{

        public Type Extend(Type baseType)
        {
            string typeName = baseType.Name + "ExtenderProxy";
            string moduleName = "Puzzle.NAspect.Runtime.ExtenderProxy";

            AssemblyBuilder assemblyBuilder = GetAssemblyBuilder();            
            TypeBuilder typeBuilder = GetTypeBuilder(assemblyBuilder, moduleName, typeName, baseType);
            Type proxyType = typeBuilder.CreateType();            
            return proxyType;
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



        private static TypeBuilder GetTypeBuilder(AssemblyBuilder assemblyBuilder, string moduleName, string typeName,Type baseType)
        {
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(moduleName);
            TypeAttributes typeAttributes = TypeAttributes.Class | TypeAttributes.Public;
            return moduleBuilder.DefineType(typeName, typeAttributes, baseType);
        }


    }
}
