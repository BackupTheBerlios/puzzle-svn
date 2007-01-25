using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework.Aop;

namespace Puzzle.NAspect.Visualization.Presentation
{
    public class PresentationPointcut : Pointcut
    {
        public PresentationPointcut(IGenericAspect aspect) 
        {
            this.aspect = aspect;
        }

        public PresentationPointcut(IGenericAspect aspect, IPointcut pointcut)
        {
            this.aspect = aspect;

            this.Name = pointcut.Name;

            foreach (PointcutTarget target in pointcut.Targets)
            {
                PresentationPointcutTarget presTarget = new PresentationPointcutTarget(this, target);
                this.Targets.Add(presTarget);
            }

            foreach (object interceptor in pointcut.Interceptors)
            {
                string typeName = "";
                if (interceptor is Type)
                    typeName = ((Type)interceptor).FullName;
                else
                    typeName = (string)interceptor;

                PresentationInterceptor presInterceptor = new PresentationInterceptor(this, typeName);
                this.Interceptors.Add(presInterceptor);
            }

        }

        private IGenericAspect aspect;
        public virtual IGenericAspect Aspect
        {
            get { return aspect; }
            set { aspect = value; }
        }
    }
}
