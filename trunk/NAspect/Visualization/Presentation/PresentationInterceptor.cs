using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NAspect.Framework.Aop;

namespace Puzzle.NAspect.Visualization.Presentation
{
    public class PresentationInterceptor
    {
        public PresentationInterceptor(IPointcut pointcut)
        {
            this.pointcut = pointcut;
        }

        public PresentationInterceptor(IPointcut pointcut, string interceptor)
        {
            this.pointcut = pointcut;
            this.typeName = interceptor;
        }

        private IPointcut pointcut;
        public virtual IPointcut Pointcut
        {
            get { return pointcut; }
            set { pointcut = value; }
        }	

        private string typeName;
        public virtual string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

    }
}
