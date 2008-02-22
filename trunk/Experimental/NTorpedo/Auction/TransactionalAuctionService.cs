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
using System.Collections;
using System.EnterpriseServices ;

//This class corresponds to the transactional part of the AuctionServiceBean.java class in the
//the original Torpedo implementation
namespace NTorpedo.Auction
{
	/// <summary>
	/// Summary description for TransactionalAuctionService. 
	/// </summary>
	[Transaction(TransactionOption.Required)]
	public class TransactionalAuctionService : ServicedComponent 
	{
		protected IPersistenceFactory persistenceFactory;
		protected Persistence persistentAuctions;

		public TransactionalAuctionService()
		{
		}

		public void SetPersistenceFactory(IPersistenceFactory persistenceFactory)
		{
			this.persistenceFactory = persistenceFactory;
			persistentAuctions = persistenceFactory.CreatePersistence() ;
		}

		[AutoComplete()]
		public string PlaceBid(String auctionId, String bidId, String userId, System.Double amount, System.Double maxAmount)
		{
			string sql = "";
			try 
			{
				IUser user = persistentAuctions.GetUser(userId);
				IAuction auction = persistentAuctions.GetAuction(auctionId);
				IBid newBid = persistentAuctions.CreateBid(bidId,auction,user,amount,maxAmount);

				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				persistentAuctions.Close();

				sql = persistentAuctions.Sql;
			} 
			catch (Exception e) 
			{
				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				if (persistentAuctions!=null) persistentAuctions.Close();
				throw new AuctionServiceException(e);
			}
			return sql;
		}

		[AutoComplete()]
		public string Place2Bids(String auctionId1, String auctionId2, String bidId1, String bidId2, String userId, System.Double amount1, System.Double amount2, System.Double maxAmount1, System.Double maxAmount2) 
		{
			string sql = "";
			try 
			{
				IUser user = persistentAuctions.GetUser(userId);
				IAuction auction1 = persistentAuctions.GetAuction(auctionId1);
				IBid newBid1 = persistentAuctions.CreateBid(bidId1,auction1,user,amount1,maxAmount1);
				IAuction auction2 = persistentAuctions.GetAuction(auctionId2);
				IBid newBid2 = persistentAuctions.CreateBid(bidId2,auction2,user,amount2,maxAmount2);

				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				persistentAuctions.Close();

				sql = persistentAuctions.Sql;

			} 
			catch (Exception e) 
			{
				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				if (persistentAuctions!=null) persistentAuctions.Close();
				throw new AuctionServiceException(e);
			}
			return sql;
		}

		[AutoComplete()]
		public AuctionInfo ListAuction(string auctionId) 
		{
			try 
			{
				IAuction auction = persistentAuctions.GetAuction(auctionId);
				AuctionInfo ai = persistentAuctions.CreateAuctionInfo(auction,false);

				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				persistentAuctions.Close();

				ai.Sql = persistentAuctions.Sql;

				return ai;
			} 
			catch (Exception e) 
			{
				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				if (persistentAuctions!=null) persistentAuctions.Close();
				throw new AuctionServiceException(e);
			}
		}

		[AutoComplete()]
		public AuctionInfo ListAuctionTwiceWithTransaction(String auctionId)
		{
			try 
			{
				TransactionalAuctionService service = new TransactionalAuctionService();
				service.SetPersistenceFactory(this.persistenceFactory);
				AuctionInfo ai = service.ListAuction(auctionId);
				
				string sql = ai.Sql;

				service = new TransactionalAuctionService();
				service.SetPersistenceFactory(this.persistenceFactory);
				ai = service.ListAuction(auctionId);

				ai.Sql = sql + ai.Sql;

				return ai;
			} 
			catch (Exception e) 
			{
				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				if (persistentAuctions!=null) persistentAuctions.Close();
				throw new AuctionServiceException(e);
			}
		}

		[AutoComplete()]
		public AuctionInfo ListPartialAuction(String auctionId) 
		{
			try 
			{
				IAuction auction = persistentAuctions.GetPartialAuction(auctionId);
				AuctionInfo ai = persistentAuctions.CreateAuctionInfo(auction,true);

				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				persistentAuctions.Close();

				ai.Sql = persistentAuctions.Sql;

				return ai;
			} 
			catch (Exception e) 
			{
				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				if (persistentAuctions!=null) persistentAuctions.Close();
				throw new AuctionServiceException(e);
			}
		}

		[AutoComplete()]
		public CollectionWithSQL FindAllAuctions() 
		{
			try 
			{
				IList auctions = persistentAuctions.FindAllAuctions();
				CollectionWithSQL result = new CollectionWithSQL(
					new ArrayList());
				foreach (IAuction a in auctions) 
					result.TheCollection.Add(persistentAuctions.CreateAuctionInfo(a,false));

				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				persistentAuctions.Close();

				result.TheSql = persistentAuctions.Sql ;
				return result;
			} 
			catch (Exception e) 
			{
				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				if (persistentAuctions!=null) persistentAuctions.Close();
				throw new AuctionServiceException(e);
			}
		}

		[AutoComplete()]
		public CollectionWithSQL FindHighBids(String auctionId) 
		{
			try {
				IList bids = persistentAuctions.FindHighBids(auctionId);
				CollectionWithSQL result = new CollectionWithSQL(
						new ArrayList());
				foreach (IBid b in bids) 
					result.TheCollection.Add(new BidInfo(b));

				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				persistentAuctions.Close();

				result.TheSql = persistentAuctions.Sql ;

				return result;
			} catch (Exception e) {
				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				if (persistentAuctions!=null) persistentAuctions.Close();
				throw new AuctionServiceException(e);
			}
	    }

	}
}
