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
using System.Reflection;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Debug.Serialization;

namespace Puzzle.NAspect.Framework
{
	public class SerializableProxyMixin : ISerializableProxy , IProxyAware
	{
        private IAopProxy target;
        #region ISerializableProxy Members

        public SerializedProxy GetSerializedProxy()
        {
            SerializedProxy proxy = new SerializedProxy();
            proxy.TypeName = target.GetType().FullName;
            return proxy;
        }

        #endregion

        #region IProxyAware Members

        public void SetProxy(IAopProxy target)
        {
            this.target = target;
        }

        #endregion
    }
}
