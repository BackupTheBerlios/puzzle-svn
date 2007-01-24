// *
// * Copyright (C) 2005 Mats Helander : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Puzzle.NAspect.Framework.Utils;
using Puzzle.NAspect.Framework.Tools;

namespace Puzzle.NAspect.Framework.Aop
{
    /// <summary>
    /// The target description for a pointcut
    /// </summary>
	public class PointcutTarget
	{
        public PointcutTarget()
        {
        }

        public PointcutTarget(string signature, PointcutTargetType targetType)
        {
            this.signature = signature;
            this.targetType = targetType;
        }

        public PointcutTarget(Type signatureType, PointcutTargetType targetType)
        {
            this.signatureType = signatureType;
            this.targetType = targetType;
        }

        private string signature = "";
        public virtual string Signature
        {
            get 
            {
                if (signature == "")
                    if (signatureType != null)
                        return signatureType.FullName;
                return signature;
            }
            set { signature = value; }
        }

        private PointcutTargetType targetType = PointcutTargetType.Signature;
        public virtual PointcutTargetType TargetType
        {
            get { return targetType; }
            set { targetType = value; }
        }

        private Type signatureType = null;
        private Type GetSignatureType()
        {
            if (signatureType == null)
            {
                if (signature != "")
                {
                    signatureType = Type.GetType(signature);
                    if (signatureType == null)
                        throw new Exception(
                            string.Format("Type '{0}' was not found!", signature));
                }
            }
            return signatureType;
        }


        public bool IsMatch(MethodBase method)
        {
            switch (this.targetType)
            {
                case PointcutTargetType.Signature:
                    return IsSignatureMatch(method);
                case PointcutTargetType.Attribute:
                    return IsAttributeMatch(method);
                default:
                    throw new Exception(String.Format("Unknown pointcut target type {0}", targetType.ToString()));
            }
        }

        /// <summary>
        /// Matches a method with the pointuct
        /// </summary>
        /// <param name="method">The method to match</param>
        /// <returns>True if the pointcut matched the method, otherwise false</returns>
        public bool IsSignatureMatch(MethodBase method)
        {
            string methodsignature = AopTools.GetMethodSignature(method);
            if (Text.IsMatch(methodsignature, signature))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Matches a method with the pointuct
        /// </summary>
        /// <param name="method">The method to match</param>
        /// <returns>True if the pointcut matched the method, otherwise false</returns>
        public bool IsAttributeMatch(MethodBase method)
        {
            Type signatureType = GetSignatureType();
            if (signatureType == null)
                return false;

            if (method.GetCustomAttributes(signatureType, true).Length > 0)
                return true;
            else
                return false;
        }
	}
}
