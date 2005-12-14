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
using NTorpedo.Client.AuctionWebService;

namespace NTorpedo.Client
{
	/// <summary>
	/// Summary description for FindHighBids.
	/// </summary>
	[TestFixture()]
	public class FindHighBids : NTorpedoClient, ITest
	{
		public static string TestName = "FindHighBids";

		public FindHighBids() : base(TestName)
		{
		}

		public FindHighBids(string url) : base(TestName, url)
		{
		}

		[Test()]
		public void RunTest() 
		{
			PrintTestTitle();
			string auctionId = "3";
			CollectionWithSQL result = AuctionService.FindHighBids(auctionId);
			PrintBids(result.TheCollection);
			PrintSQL(result.TheSql);
			CloseResult();
		}	
	}
}
