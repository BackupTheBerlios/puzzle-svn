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
using Puzzle.NAspect.Framework.Interception;

namespace Puzzle.NAspect.Framework.Aop
{
	public class TypedToGenericConverter
	{
        public static IGenericAspect Convert(ITypedAspect aspect)
        {
            IGenericAspect newAspect = null;

            IList mixins = new ArrayList();
            IList pointcuts = new ArrayList();

            AddMixins(aspect, mixins);

            MethodInfo[] methods = aspect.GetType().GetMethods( BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo method in methods)
            {
                object[] interceptorAttributes = method.GetCustomAttributes(typeof(InterceptorAttribute), false);
                if (interceptorAttributes != null)
                {
                    InterceptorAttribute interceptor = (InterceptorAttribute)interceptorAttributes[0];
                    IPointcut pointcut = null;
                    Delegate interceptorDelegate = CreateDelegate(aspect, method);
                    if (interceptor.TargetAttribute != null)
                    {
                        pointcut = new AttributePointcut(interceptor.TargetAttribute, interceptorDelegate);
                    }
                    else if (interceptor.TargetSignature != null)
                    {
                        pointcut = new SignaturePointcut(interceptor.TargetSignature, interceptorDelegate);
                    }
                    else
                    {
                        throw new Exception("Interceptor attribute does not contain any target info");
                    }
                    pointcuts.Add(pointcut);
                }                
            }


            newAspect = CreateAspect(aspect, newAspect, mixins, pointcuts);

            return newAspect;
        }

        private static Delegate CreateDelegate(ITypedAspect aspect, MethodInfo method)
        {
            Delegate interceptorDelegate = null;
            Type paramType = method.GetParameters()[0].ParameterType;
            if (paramType == typeof(MethodInvocation))
            {
                interceptorDelegate = Delegate.CreateDelegate(typeof(AroundDelegate), aspect, method.Name);
            }
            else if (paramType == typeof(AfterMethodInvocation))
            {
                interceptorDelegate = Delegate.CreateDelegate(typeof(AfterDelegate), aspect, method.Name);
            }
            else if (paramType == typeof(BeforeMethodInvocation))
            {
                interceptorDelegate = Delegate.CreateDelegate(typeof(BeforeDelegate), aspect, method.Name);
            }
            else
            {
                throw new Exception("Unknown interceptor delegate");
            }
            return interceptorDelegate;
        }

        private static IGenericAspect CreateAspect(ITypedAspect aspect, IGenericAspect newAspect, IList mixins, IList pointcuts)
        {
            object[] aspectTargetAttributes = aspect.GetType().GetCustomAttributes(typeof(AspectTargetAttribute), false);
            if (aspectTargetAttributes != null)
            {
                AspectTargetAttribute aspectTargetAttribute = (AspectTargetAttribute)aspectTargetAttributes[0];
                if (aspectTargetAttribute.TargetAttribute != null)
                    newAspect = new AttributeAspect(aspect.GetType().Name, aspectTargetAttribute.TargetAttribute, mixins, pointcuts);
                else if (aspectTargetAttribute.TargetSignature != null)
                    newAspect = new SignatureAspect(aspect.GetType().Name, aspectTargetAttribute.TargetSignature, mixins, pointcuts);
                else if (aspectTargetAttribute.TargetType != null)
                    newAspect = new SignatureAspect(aspect.GetType().Name, aspectTargetAttribute.TargetType, mixins, pointcuts);
                else
                    throw new Exception("No target specified");
            }
            return newAspect;
        }

        private static void AddMixins(ITypedAspect aspect, IList mixins)
        {
            object[] mixinAttributes = aspect.GetType().GetCustomAttributes(typeof(MixinAttribute), false);
            if (mixinAttributes != null)
            {
                foreach (MixinAttribute mixinAttribute in mixinAttributes)
                {
                    mixins.Add(mixinAttribute.MixinType);
                }
            }
        }
	}
}
