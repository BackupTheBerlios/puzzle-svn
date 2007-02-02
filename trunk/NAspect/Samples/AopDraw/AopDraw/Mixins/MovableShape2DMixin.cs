using System;
using System.Collections.Generic;
using System.Text;
using AopDraw.Interfaces;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Framework;
using AopDraw.Classes.Shapes;

namespace AopDraw.Mixins
{
    public class MovableShape2DMixin : IMovable , IProxyAware
    {
        private Shape2D shape;

        public virtual void MoveTo(double x, double y)
        {
            shape.X = x;
            shape.Y = y;
        }        

        public void SetProxy(IAopProxy target)
        {
            Shape2D shape = target as Shape2D;

            if (shape == null)
                throw new ArgumentException("target is not an IShape2D");

            this.shape = shape;
        }        
    }
}
