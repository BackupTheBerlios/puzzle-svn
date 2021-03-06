using System;
using Puzzle.NPresent.Framework.ViewModel;
// *
// * Copyright (C) 2005 Mats Helander
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

namespace Puzzle.NPresent.Framework.Wrapping
{
	/// <summary>
	/// Summary description for Wrapper.
	/// </summary>
	public class Wrapper : IWrapper
	{
		public Wrapper()
		{
			
		}


		public IWrappedObject WrapObject(object obj, ClassView classView)
		{
			IWrappedObject wrappedObject = new WrappedObject(obj, classView);

			return wrappedObject;
		}
	}
}
