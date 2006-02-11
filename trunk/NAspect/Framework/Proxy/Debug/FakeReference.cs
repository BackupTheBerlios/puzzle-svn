// *
// * Copyright (C) 2005 Roger Johansson : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

#if NET2 && DEBUG
using System;
using System.Collections;
using System.Reflection;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Debug.Serialization;
using Puzzle.NAspect.Debug.Serialization.Elements;
using Puzzle.NAspect.Framework.Interception;

//this type is not used for real
//its only here in order to get the debug project copied to your bin dir when debugging
//the debug assembly can safely be removed when you go live..

//this is also only available in debug builds
namespace Puzzle.NAspect.Framework
{
	public class FakeReference
	{
        //just make a ref to the debug assembly
        private Type t = typeof(Puzzle.NAspect.Debug.AopProxyObjectSource);
	}
}
#endif