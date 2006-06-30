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
//#if NET2
//using Puzzle.NAspect.Debug.Serialization;
//#endif

namespace Puzzle.NAspect.Framework
{
    /// <summary>
    /// Interface implemented by all subclass and interface proxies.
    /// <seealso cref="AopProxyMixin"/>
    /// </summary>
    public interface IAopProxy
    {
        /// <summary>
        /// Custom data associated with the proxy instance
        /// </summary>
        IDictionary Data { get; }

        /// <summary>
        /// This is one of the key methods of the entire interception process.
        /// This method handles calls from the proxy and redirects them to the interceptors.
        /// </summary>
        /// <param name="target">The proxy instance on which the call was invoked</param>
        /// <param name="methodId">Unique identifier of the method</param>
        /// <param name="parameters">Untyped list of <c>InterceptedParameter</c>s</param>
        /// <param name="returnType">The return type of the invoked method</param>
        /// <returns>The result of the call chain</returns>
        object HandleCall(IAopProxy target, object executionTarget, string methodId, IList parameters, Type returnType);
    }
}