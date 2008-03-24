using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NContext.Framework;
using System.Collections;

namespace Puzzle.NContext.Framework
{
    public class TemplateAspect : GenericAspect
    {
        public TemplateAspect() : base ("NContextAspect")
        {
            //target all classes implementing ITemplate
            Targets.Add(new AspectTarget(typeof(ITemplate), AspectTargetType.Interface));
            //target all methods marked with FactoryMethodAttribute
            Pointcuts.Add (new AttributePointcut(typeof(FactoryMethodAttribute), new FactoryMethodInterceptor()));
        }
    }
}
