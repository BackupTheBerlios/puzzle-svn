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
using NUnit.Framework;
using NTorpedo.Auction;

namespace NTorpedo.Tests
{
	/// <summary>
	/// Summary description for AuctionTests.
	/// </summary>'
	[TestFixture()]
	public class AuctionTests
	{
		public AuctionTests()
		{
		}

		[Test()]
		public void ListAuctionTest()
		{
			AuctionTestService svc = new AuctionTestService() ;
			
			AuctionInfo ai = svc.ListAuction("3");

			Assert.IsNotNull(ai);

			PrintAuction(ai);
		}

		[Test()]
		public void ListAuctionTwiceWithoutTransactionTest()
		{
			AuctionTestService svc = new AuctionTestService() ;
			
			AuctionInfo ai = svc.ListAuctionTwiceWithoutTransaction("3");

			Assert.IsNotNull(ai);

			PrintAuction(ai);
		}

		[Test()]
		public void ListAuctionTwiceWithTransactionTest()
		{
			AuctionTestService svc = new AuctionTestService() ;
			
			AuctionInfo ai = svc.ListAuctionTwiceWithTransaction("3");

			Assert.IsNotNull(ai);

			PrintAuction(ai);
		}

		protected void PrintAuction(AuctionInfo ai)
		{
			Console.WriteLine("Auction: " + ai.AuctionID );
			Console.WriteLine("Description: " + ai.Description );
		}
	
	}
}
