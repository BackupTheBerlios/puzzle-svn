// *
// * Copyright (C) 2005 Roger Johansson : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System.Collections;

namespace Puzzle.NAspect.Framework.ConfigurationElements
{
	public class EngineConfiguration
	{
		#region Public Property Aspects

		private IList aspects;

		public IList Aspects
		{
			get { return this.aspects; }
			set { this.aspects = value; }
		}

		#endregion

		#region Public Property Name

		private string name;

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		#endregion

		public EngineConfiguration()
		{
			aspects = new ArrayList();
		}
	}
}