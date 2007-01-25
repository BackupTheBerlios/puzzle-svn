using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Visualization.Presentation;

namespace Puzzle.NAspect.Visualization.Nodes
{
    public class PointcutNode : NodeBase
    {
        public PointcutNode(IPointcut pointcut)
            : base(pointcut.Name)
        {
            this.pointcut = pointcut;

            this.ImageIndex = 6;
            this.SelectedImageIndex = 6;

            foreach (PointcutTarget target in pointcut.Targets)
            {
                PointcutTargetNode targetNode = new PointcutTargetNode(target);
                this.Nodes.Add(targetNode);
            }

            foreach (PresentationInterceptor interceptor in pointcut.Interceptors)
            {
                InterceptorNode interceptorNode = new InterceptorNode(interceptor);
                this.Nodes.Add(interceptorNode);
            }
        }

        private IPointcut pointcut;
        public virtual IPointcut Pointcut
        {
            get { return pointcut; }
            set { pointcut = value; }
        }

        public override object Object
        {
            get { return this.pointcut ; }
        }

        public override void Refresh()
        {
            this.Text = this.pointcut.Name;

            base.Refresh();
        }

    }	
}
