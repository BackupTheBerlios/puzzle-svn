using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Puzzle.NAspect.Framework.Aop
{
    /// <summary>
    /// The standard aspect representation
    /// </summary>
	public class GenericAspect : GenericAspectBase
	{       
        /// <summary>
        /// Attribute aspect Ctor.
        /// </summary>
        /// <param name="Name">Name of the aspect.</param>
        /// <param name="mixins">IList of mixin types.</param>
        /// <param name="pointcuts">IList of IPointcut instances.</param>
        public GenericAspect(string Name, IList mixins, IList pointcuts)
        {
            this.Name = Name;
            Mixins = mixins;
            Pointcuts = pointcuts;
        }

        /// <summary>
        /// Attribute aspect Ctor.
        /// </summary>
        /// <param name="Name">Name of the aspect.</param>
        /// <param name="mixins">Type[] array of mixin types</param>
        /// <param name="pointcuts">IPointcut[] array of pointcut instances</param>
        public GenericAspect(string Name, Type[] mixins, IPointcut[] pointcuts)
        {
            this.Name = Name;
            Mixins = new ArrayList(mixins);
            Pointcuts = new ArrayList(pointcuts);
        }

        /// <summary>
        /// Attribute aspect Ctor.
        /// </summary>
        /// <param name="Name">Name of the aspect.</param>
        /// <param name="targets">IList of aspect targets.</param>
        /// <param name="mixins">IList of mixin types.</param>
        /// <param name="pointcuts">IList of IPointcut instances.</param>
        public GenericAspect(string Name, IList targets, IList mixins, IList pointcuts)
        {
            this.Name = Name;
            Targets = targets;
            Mixins = mixins;
            Pointcuts = pointcuts;
        }

        /// <summary>
        /// Attribute aspect Ctor.
        /// </summary>
        /// <param name="Name">Name of the aspect.</param>
        /// <param name="targets">Type[] array of aspect targets</param>
        /// <param name="mixins">Type[] array of mixin types</param>
        /// <param name="pointcuts">IPointcut[] array of pointcut instances</param>
        public GenericAspect(string Name, AspectTarget[] targets, Type[] mixins, IPointcut[] pointcuts)
        {
            this.Name = Name;
            Targets = new ArrayList(targets);
            Mixins = new ArrayList(mixins);
            Pointcuts = new ArrayList(pointcuts);
        }

    }
}
