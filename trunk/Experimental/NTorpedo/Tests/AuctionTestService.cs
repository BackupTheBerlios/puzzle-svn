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
	/// Summary description for AuctionTestService.
	/// </summary>
	public class AuctionTestService
	{
		public AuctionTestService()
		{
		}

		protected TransactionalAuctionService GetTransactionalAuctionService()
		{
			IPersistenceFactory persistenceFactory = GetPersistenceFactory();
			TransactionalAuctionService service =  new TransactionalAuctionService() ; 			
			service.SetPersistenceFactory(persistenceFactory);
			return service;
		}

		protected NonTransactionalAuctionService GetNonTransactionalAuctionService()
		{
			IPersistenceFactory persistenceFactory = GetPersistenceFactory();
			NonTransactionalAuctionService service =  new NonTransactionalAuctionService() ; 			
			service.SetPersistenceFactory(persistenceFactory);
			return service;
		}

		protected IPersistenceFactory GetPersistenceFactory()
		{
			return new NTorpedo.Auction.NPersist.PersistenceFactory();
		}

		public string PlaceBid(string auctionId, string bidId, string userId, System.Double amount, System.Double maxAmount)
		{
			ClearLog();
			GetTransactionalAuctionService().PlaceBid(auctionId,bidId,userId,amount,maxAmount);
			return GetLog();
		}

		public string Place2Bids(string auctionId1, string auctionId2, string bidId1, string bidId2, string userId, System.Double amount1, System.Double amount2, System.Double maxAmount1, System.Double maxAmount2) 
		{
			ClearLog();
			GetTransactionalAuctionService().Place2Bids(auctionId1, auctionId2, bidId1,  bidId2,  userId,  amount1,  amount2,  maxAmount1,  maxAmount2);
			return GetLog();
		}

		public AuctionInfo ListAuction(string auctionId) 
		{
			ClearLog();
			AuctionInfo ai=GetTransactionalAuctionService().ListAuction(auctionId);
			ai.Sql = GetLog();
			return ai;
		}

		public AuctionInfo ListAuctionTwiceWithTransaction(string auctionId)
		{
			ClearLog();
			AuctionInfo ai=GetTransactionalAuctionService().ListAuctionTwiceWithTransaction(auctionId);
			ai.Sql = GetLog();
			return ai;
		}

		public AuctionInfo ListAuctionTwiceWithoutTransaction(string auctionId)
		{
			ClearLog();
			AuctionInfo ai=GetNonTransactionalAuctionService().ListAuctionTwiceWithoutTransaction(auctionId);
			ai.Sql = GetLog();
			return ai;
		}

		public AuctionInfo listPartialAuction(string auctionId) 
		{
			ClearLog();
			AuctionInfo ai=GetTransactionalAuctionService().ListPartialAuction(auctionId);
			ai.Sql = GetLog();
			return ai;
		}

		public CollectionWithSQL FindAllAuctions()
		{
			ClearLog();
			CollectionWithSQL cwsq = GetTransactionalAuctionService().FindAllAuctions();
			cwsq.TheSql = GetLog();
			return cwsq;
		}


		public CollectionWithSQL FindHighBids(string auctionId)
		{
			ClearLog();
			CollectionWithSQL cwsq = GetTransactionalAuctionService().FindHighBids(auctionId);
			cwsq.TheSql = GetLog();
			return cwsq;
		}

		public void ClearLog()
		{
			
		}

		public string GetLog()
		{
			return "";
		}

	}
}
