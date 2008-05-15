using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Interception;
using System.Reflection;

namespace NObjectStore
{
    class EntityCtorInterceptor : IAroundInterceptor
    {
        public object HandleCall(Puzzle.NAspect.Framework.MethodInvocation call)
        {
            InterceptedParameter stateParameter = (InterceptedParameter)call.Parameters[0];
            //get the context (state) param
            object state = stateParameter.Value;

            IPersistentObject managed = (IPersistentObject)call.Target;
            
            //assign the context to the target object
            managed.Context = (Context)state;

            Initialize(call.Target as IPersistentObject);

            object res = call.Proceed();

            CheckIntegrity();

            
            return res;
        }

        private void Initialize(IPersistentObject target)
        {            
            Type type = target.GetType().BaseType;
            PropertyInfo[] properties = type.GetProperties();
            target.Initializing = true;

            foreach (PropertyInfo property in properties)
            {
                Type propertyType = property.PropertyType;

                if (propertyType.IsGenericType && propertyType.IsInterface)
                {
                    InjectInterceptableList(target, property, propertyType);
                }	
            }
            target.Initializing = false;
        }

        private static void InjectInterceptableList(IPersistentObject target, PropertyInfo property, Type propertyType)
        {
            Type subType = propertyType.GetGenericArguments()[0];
            Type genericType = typeof(PersistentList<>).MakeGenericType(subType);
            Object instance = Activator.CreateInstance(genericType);
            property.SetValue(target, instance, null);

            IPersistentList persistentList = (IPersistentList)instance;
            persistentList.Owner = target;
        }

        private void CheckIntegrity()
        {

        }
    }
}
