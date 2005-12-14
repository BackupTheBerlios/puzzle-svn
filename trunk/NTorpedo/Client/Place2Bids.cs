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
using NUnit.Framework;

namespace NTorpedo.Client
{
	/// <summary>
	/// Summary description for Place2Bids.
	/// </summary>
	[TestFixture()]
	public class Place2Bids  : NTorpedoClient, ITest
	{
		public static string TestName = "Place2Bids";

		public Place2Bids() : base(TestName)
		{
		}

		public Place2Bids(string url) : base(TestName, url)
		{
		}

		[Test()]
		public void RunTest() 
		{
			PrintTestTitle();
			string auctionId1="1";
			string auctionId2="2";
			string bidId1="26";
			string bidId2="27";
			string bidder="Mark Smith";
			float amount1 = 650000;
			float amount2 = 500;
			float maxAmount1 = 750000;
			float maxAmount2 = 650;
			string sql = AuctionService.Place2Bids(auctionId1, auctionId2, bidId1, bidId2 ,bidder,amount1, amount2,maxAmount1,maxAmount2);
			PrintSQL(sql);
			CloseResult();
		}	

	}
}
