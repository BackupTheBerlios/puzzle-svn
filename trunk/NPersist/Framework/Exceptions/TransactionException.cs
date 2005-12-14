// *
// * Copyright (C) 2005 Mats Helander
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using System.Runtime.Serialization;

namespace Puzzle.NPersist.Framework.Exceptions
{
	public class TransactionException : NPersistException
	{
		public TransactionException() : base()
		{
		}

		public TransactionException(string message) : base(message)
		{
		}

		public TransactionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public TransactionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}