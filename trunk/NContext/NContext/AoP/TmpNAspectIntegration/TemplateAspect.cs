using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework.Aop;
using Mojo;
using System.Collections;

namespace Mojo
{
    public class TemplateAspect : GenericAspect
    {
        public TemplateAspect() : base ("Mojo.Aspect")
        {
            //target all classes implementing ITemplate
            Targets.Add(new AspectTarget(typeof(ITemplate), AspectTargetType.Interface));
            //target all methods marked with FactoryMethodAttribute
            Pointcuts.Add (new AttributePointcut(typeof(FactoryMethodAttribute), new FactoryMethodInterceptor()));
        }
    }
}
