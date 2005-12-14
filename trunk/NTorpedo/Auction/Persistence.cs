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
using System.Collections;

namespace NTorpedo.Auction
{
	/// <summary>
	/// Summary description for Persistence.
	/// </summary>
	public abstract class Persistence
	{
		public abstract IBid CreateBid(string id, IAuction auction, IUser buyer, System.Double amount, System.Double maxAmount) ;

		public abstract IAuction GetAuction(string auctionId);
		
		public abstract IUser GetUser(string id);
		
		public abstract IList FindAllAuctions();
		
		public abstract IList FindHighBids(string auctionId);
		
		public virtual IAuction GetPartialAuction(string auctionId)
		{
			// default behavior is just to get the auction.  O-R mapping specific classes
			// may want to override the method to do an optimization
			return GetAuction(auctionId);
		}

		public AuctionInfo CreateAuctionInfo(IAuction auction, bool partial) 
		{
			return new AuctionInfo(auction, partial);
		}
		
		public BidInfo CreateBidInfo(IBid bid) 
		{
			return new BidInfo(bid);
		}
		
		// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
		// Those that do not simply ignore this operation.
		public virtual void Close() {}

		#region Property  Sql
		
		private string sql = "";
		
		public string Sql
		{
			get { return this.sql; }
			set { this.sql = value; }
		}
		
		#endregion

	}
}
