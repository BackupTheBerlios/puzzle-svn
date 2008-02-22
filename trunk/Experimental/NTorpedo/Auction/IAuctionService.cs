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

//unfortunately, since .NET EnterpriseServices does not allow that some methods in a transactional class
//are marked as non-transactional, the AuctionService implementation had to be split up into two classes:
//TransactionalAuctionService and NonTransactionalAuctionService. Thus, there is actually no class implementing
//this interface. A class wrapping TransactionalAuctionService and NonTransactionalAuctionService and thereby
//implementing this interface could be added, but isn't really needed for the application to work.
namespace NTorpedo.Auction
{
	/// <summary>
	/// Summary description for IAuctionService.
	/// </summary>
	public interface IAuctionService
	{
		string PlaceBid(string auctionID, string bidID, string userID, System.Double amount, System.Double maxAmount);
		string Place2Bids(string auctionID1, string auctionID2, string bidID1, string bidID2, string userID, System.Double amount1, System.Double amount2, System.Double maxAmount1, System.Double maxAmount2);
		AuctionInfo ListAuction(string auctionID);
		AuctionInfo ListAuctionTwiceWithTransaction(string auctionID);
		AuctionInfo ListAuctionTwiceWithoutTransaction(string auctionID);
		AuctionInfo ListPartialAuction(string auctionID);
		CollectionWithSQL FindAllAuctions();
		CollectionWithSQL FindHighBids(string auctionID);
	}
}
