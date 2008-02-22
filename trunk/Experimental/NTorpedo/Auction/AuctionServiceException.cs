/*
* ====================================================================
*
* NTORPEDO
* A Testbed of Object Relational Products for Enterprise Distributed Objects
* Copyright (c) 2005 torpedo-group
* http://www.torpedo-group.org
* @author Bruce Martin
* @version 8.25.04
* Translated to c# by Mats Helander
*
* This library is free software; you can redistribute it and/or modify it
* under the terms of the GNU Lesser General Public License 2.1 or later, as
* published by the Free Software Foundation. See the included license.txt
* or http://www.gnu.org/copyleft/lesser.html for details.
* ====================================================================
*/

using System;
//I'm assuming that this exception in the original Torpedo implementation is only there
//to work around the horrible java "throws" declaration requirement, in .NET this is
//just a typically redundant "MeTooException" without any purpose clear to me...still, 
//just to be safe, here it is!
namespace NTorpedo.Auction
{
	/// <summary>
	/// Summary description for AuctionServiceException.
	/// </summary>
	public class AuctionServiceException : Exception
	{
		public AuctionServiceException()
		{
		}

		public AuctionServiceException(string message) : base(message)
		{
		}

		public AuctionServiceException(Exception ex) : base(ex.Message)
		{
		}
	}
}
