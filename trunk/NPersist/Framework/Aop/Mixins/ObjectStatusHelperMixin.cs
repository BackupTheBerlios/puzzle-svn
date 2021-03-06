using System;
using System.Diagnostics;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Framework.Interfaces;
// *
// * Copyright (C) 2005 Roger Alsing : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *


namespace Puzzle.NPersist.Framework.Aop.Mixins
{
	public class ObjectStatusHelperMixin : IObjectStatusHelper
	{
		//private ObjectStatus status = ObjectStatus.UpForCreation;
		private ObjectStatus status = ObjectStatus.NotRegistered;

		public ObjectStatusHelperMixin(object target)
		{
		}

		public ObjectStatusHelperMixin()
		{
		}

		
		public ObjectStatus GetObjectStatus()
		{
			return status;
		}

		public void SetObjectStatus(ObjectStatus value)
		{
			status = value;
		}
	}
}
