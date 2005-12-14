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
	/// Summary description for PlaceBid.
	/// </summary>
	[TestFixture()]
	public class PlaceBid : NTorpedoClient, ITest
	{
		public static string TestName = "PlaceBid";

		public PlaceBid() : base(TestName)
		{
		}

		public PlaceBid(string url) : base(TestName, url)
		{
		}

		[Test()]
		public void RunTest() 
		{
			PrintTestTitle();
			string auctionID="3";
			string bidId="25";
			string bidder="Bruce Martin";
			float amount = 2500;
			float maxAmount = 9500;
			string sql = AuctionService.PlaceBid(auctionID,bidId,bidder,amount,maxAmount);
			PrintSQL(sql);
			CloseResult();
		}	

	}
}
