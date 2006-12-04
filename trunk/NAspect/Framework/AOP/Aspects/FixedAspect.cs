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
using System.Reflection.Emit;
using Puzzle.NAspect.Framework.Interception;
using Puzzle.NAspect.Framework.Tools;
using System.Reflection;

namespace Puzzle.NAspect.Framework.Aop
{
    /// <summary>    
    /// </summary>
    public class FixedAspect : GenericAspectBase
    {
        public FixedAspect()
        {
            FixedPointcut autoPointcut = new FixedPointcut();
            this.Pointcuts.Add(autoPointcut);            
        }        


        public override bool IsMatch(Type type)
        {
            object[] attribs = null;
            attribs = type.GetCustomAttributes(typeof(ApplyInterceptorAttribute),true);
            if (attribs.Length > 0)
                return true;

            attribs = type.GetCustomAttributes(typeof(ApplyMixinAttribute), true);
            if (attribs.Length > 0)
                return true;


            MemberInfo[] members = type.GetMembers();
            foreach (MemberInfo member in members)
            {
                attribs = member.GetCustomAttributes(typeof(ApplyInterceptorAttribute), true);
                if (attribs.Length > 0)
                    return true;
            }

            return false;
        }
    }
}