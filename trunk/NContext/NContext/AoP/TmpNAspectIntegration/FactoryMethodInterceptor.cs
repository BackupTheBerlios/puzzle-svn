using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework.Interception;
using Puzzle.NAspect.Framework;
using System.Reflection;
using System.Linq;

namespace Mojo
{
    public class FactoryMethodInterceptor : IAroundInterceptor
    {
        public object HandleCall(MethodInvocation call)
        {
            ITemplate template = call.Target as ITemplate;
            IContext context = template.Context;

            lock (context.State)
            {
                FactoryMethodAttribute attrib = call.Method.GetCustomAttributes(typeof(FactoryMethodAttribute), true).FirstOrDefault () as FactoryMethodAttribute;
                if (attrib == null)
                    return call.Proceed(); //not a factory method;

                if (attrib.RegisterAs == FactoryType.DefaultForType)
                {
                    return TypedFactoryCall(call, attrib,context);
                }
                else
                {
                    return NamedFactoryCall(call, attrib,context);
                }
            }
        }

        private string GetFactoryId(MethodInvocation call, FactoryMethodAttribute attrib)
        {
            string factoryId = null;
            if (attrib.FactoryId != null)
                factoryId = attrib.FactoryId;
            else
                factoryId = call.Method.Name;

            return factoryId;
        }

        private Type GetFactoryType(MethodInvocation call, FactoryMethodAttribute attrib)
        {
            MethodInfo method = call.Method as MethodInfo;
            return method.ReturnType;
        }

        private object NamedFactoryCall(MethodInvocation call, FactoryMethodAttribute attrib,IContext context)
        {
            string factoryId = GetFactoryId(call, attrib);

            //get from context cache
            if (attrib.InstanceMode == InstanceMode.PerContext && context.State.namedPerContextObjects.ContainsKey(factoryId))
                return context.State.namedPerContextObjects[factoryId];

            //get from graph cache
            if (attrib.InstanceMode == InstanceMode.PerGraph && context.State.namedPerGraphObjects.ContainsKey(factoryId))
                return context.State.namedPerGraphObjects[factoryId];

            //get from thread cache
            if (attrib.InstanceMode == InstanceMode.PerThread && context.State.namedPerThreadObjects.ContainsKey(factoryId))
                return context.State.namedPerThreadObjects[factoryId];

            object res = call.Proceed();

            if (res is IRunnable)
                RunnableEngine.RunRunnable(res as IRunnable);

            //add to context cache
            if (attrib.InstanceMode == InstanceMode.PerContext)
                context.State.namedPerContextObjects.Add(factoryId, res);

            //add to graph cache
            if (attrib.InstanceMode == InstanceMode.PerGraph)
                context.State.namedPerGraphObjects.Add(factoryId, res);

            //add to thread cache
            if (attrib.InstanceMode == InstanceMode.PerThread)
                context.State.namedPerThreadObjects.Add(factoryId, res);

            return res;
        }

        private object TypedFactoryCall(MethodInvocation call, FactoryMethodAttribute attrib,IContext context)
        {
            Type factoryType = GetFactoryType(call, attrib);
            

            //get from context cache
            if (attrib.InstanceMode == InstanceMode.PerContext && context.State.typedPerContextObjects.ContainsKey(factoryType))
                return context.State.typedPerContextObjects[factoryType];

            //get from graph cache
            if (attrib.InstanceMode == InstanceMode.PerGraph && context.State.typedPerGraphObjects.ContainsKey(factoryType))
                return context.State.typedPerGraphObjects[factoryType];

            //get from thread cache
            if (attrib.InstanceMode == InstanceMode.PerThread && context.State.typedPerThreadObjects.ContainsKey(factoryType))
                return context.State.typedPerThreadObjects[factoryType];

            object res = call.Proceed();

            if (res is IRunnable)
                RunnableEngine.RunRunnable(res as IRunnable);

            //add to context cache
            if (attrib.InstanceMode == InstanceMode.PerContext)
                context.State.typedPerContextObjects.Add(factoryType, res);

            //add to graph cache
            if (attrib.InstanceMode == InstanceMode.PerGraph)
                context.State.typedPerGraphObjects.Add(factoryType, res);

            //add to thread cache
            if (attrib.InstanceMode == InstanceMode.PerThread)
                context.State.typedPerThreadObjects.Add(factoryType, res);

            return res;
        }
    }
}
