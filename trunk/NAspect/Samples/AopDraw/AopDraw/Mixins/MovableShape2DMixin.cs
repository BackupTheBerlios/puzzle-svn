using System;
using System.Collections.Generic;
using System.Text;
using AopDraw.Interfaces;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Framework;

namespace AopDraw.Mixins
{
    public class MovableShape2DMixin : IMovable , IProxyAware
    {
        private IShape2D shape;

        public virtual void MoveTo(double x, double y)
        {
            shape.X = x;
            shape.Y = y;
        }        

        public void SetProxy(IAopProxy target)
        {
            IShape2D shape = target as IShape2D;

            if (shape == null)
                throw new ArgumentException("target is not an IShape2D");

            this.shape = shape;
        }        
    }
}
