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
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Framework.ConfigurationElements;
using Puzzle.NCore.Framework.Logging;
#if NET2
using Puzzle.NAspect.Debug.Serialization;
#endif

namespace Puzzle.NAspect.Framework
{
    public interface IEngine
    {
        object[] AddStateToCtorParams(object state, object[] args);
        Puzzle.NAspect.Framework.ConfigurationElements.EngineConfiguration Configuration { get; set; }
        
        object CreateProxy(Type type, params object[] args);
        Type CreateProxyType(Type type);
        
#if NET2
        T CreateProxy<T>(params object[] args);
        T CreateProxyWithState<T>(object state, params object[] args);
#endif

        object CreateProxyWithState(object state, Type type, params object[] args);
        object CreateWrapper(object instance);
        Type CreateWrapperType(Type type);
        Puzzle.NCore.Framework.Logging.ILogManager LogManager { get; set; }
    }
}
