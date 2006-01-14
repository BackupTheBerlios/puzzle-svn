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
	public abstract class AspectBase : IAspect
	{
		private string name;
		private IList mixins = new ArrayList();
		private IList pointcuts = new ArrayList();

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public abstract bool IsMatch(Type type);


		public IList Mixins
		{
			get { return mixins; }
			set { mixins = value; }
		}

		public IList Pointcuts
		{
			get { return pointcuts; }
			set { pointcuts = value; }
		}

	}
}