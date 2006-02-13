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
            newAspect = CreateAspect(aspect, newAspect, mixins, pointcuts);

            return newAspect;
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
