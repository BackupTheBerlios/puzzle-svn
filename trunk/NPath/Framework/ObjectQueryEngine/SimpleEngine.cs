// *
// * Copyright (C) 2005 Roger Johansson : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using Puzzle.NPath.Framework.CodeDom;
using System.Collections;

namespace Puzzle.NPath.Framework
{
    public class SimpleEngine
    {
        private IObjectQueryEngine engine;

        public SimpleEngine()
        {
            engine = new ObjectQueryEngine();
            engine.ObjectQueryEngineHelper = new DefaultObjectQueryEngineHelper();
        }

        public IList Select (string npathQuery,IList sourceList)
        {
            npathQuery = "select * from tmp " + npathQuery;
            return engine.GetObjectsByNPath (npathQuery,sourceList);
        }
    }
}
