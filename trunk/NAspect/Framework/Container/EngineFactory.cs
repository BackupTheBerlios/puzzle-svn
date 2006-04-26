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


namespace Puzzle.NAspect.Framework
{
	public class EngineFactory
	{
        public static IEngine FromAppConfig()
        {
            return ApplicationContext.Configure();
        }

        public static IEngine FromAppConfig(string subSectionName)
        {
            return ApplicationContext.Configure();
        }

        public static IEngine FromFile(string fileName)
        {
            return null;
        }

        public static IEngine FromFile(string fileName, string subSectionName)
        {
            return null;
        }
	}
}