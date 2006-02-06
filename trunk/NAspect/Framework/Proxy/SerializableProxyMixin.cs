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
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Debug.Serialization;
using Puzzle.NAspect.Debug.Serialization.Elements;

namespace Puzzle.NAspect.Framework
{
	public class SerializableProxyMixin : ISerializableProxy , IProxyAware
	{
        private IAopProxy target;
        #region ISerializableProxy Members

        public SerializedProxy GetSerializedProxy()
        {
            SerializedProxy proxy = new SerializedProxy();

            VizType vizType = new VizType();
            vizType.Name = target.GetType().Name;
            vizType.FullName = target.GetType().FullName;

            IList mixins = (IList)MethodCache.mixinsLookup[target.GetType()];
            foreach (Type mixinType in mixins)
            {
                VizMixin vizMixin = new VizMixin();
                vizMixin.TypeName = mixinType.Name;
                vizMixin.FullTypeName = mixinType.FullName;
                vizType.Mixins.Add(vizMixin);
            }
            IList aspects = (IList)MethodCache.aspectsLookup[target.GetType()];
            foreach (IAspect aspect in aspects)
            {
                VizAspect vizAspect = new VizAspect();
                vizAspect.Name = aspect.Name;
            }
            IList methods = (IList)MethodCache.methodsLookup[target.GetType()];
            foreach (string methodId in methods)
            {
                MethodBase methodBase = (MethodBase)MethodCache.methodLookup[methodId];
                if (methodBase is ConstructorInfo)
                {
                    ConstructorInfo constructor = (ConstructorInfo)methodBase;
                    VizConstructor vizConstructor = new VizConstructor();
                    vizConstructor.Name = constructor.Name;

                    IList interceptors = (IList)MethodCache.methodInterceptorsLookup[methodId];

                    SerializeInterceptors(vizConstructor, interceptors);
                    vizType.Methods.Add(vizConstructor);
                }
                else if (methodBase is MethodInfo)
                {
                    MethodInfo method = (MethodInfo)methodBase;
                    VizMethod vizMethod = new VizMethod();
                    vizMethod.Name = method.Name;

                    IList interceptors = (IList)MethodCache.methodInterceptorsLookup[methodId];

                    SerializeInterceptors(vizMethod, interceptors);

                    vizType.Methods.Add(vizMethod);
                }
            }



            proxy.ProxyType = vizType;
            return proxy;
        }

        private void SerializeInterceptors(VizMethodBase vizMethodBase,IList interceptors)
        {
            foreach (object interceptor in interceptors)
            {
                VizInterceptorType interceptorType = VizInterceptorType.Around;
                if (interceptor is IAfterInterceptor)
                {
                    interceptorType = VizInterceptorType.After;
                }
                else if (interceptor is IBeforeInterceptor)
                {
                    interceptorType = VizInterceptorType.Before;
                }
                else if (interceptor is IAroundInterceptor)
                {
                    interceptorType = VizInterceptorType.Around;
                }

                VizInterceptor vizInterceptor = new VizInterceptor();
                vizInterceptor.TypeName = interceptor.GetType().Name;
                vizInterceptor.FullTypeName = interceptor.GetType().FullName;

                vizMethodBase.Interceptors.Add(vizInterceptor);
            }

        }

        #endregion

        #region IProxyAware Members

        public void SetProxy(IAopProxy target)
        {
            this.target = target;
        }

        #endregion
    }
}
