using System;
using System.Collections;
using System.Data;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Framework.Delegates;
using Puzzle.NPersist.Framework.EventArguments;
using Puzzle.NPersist.Framework.Querying;

namespace NTorpedo.Auction.NPersist
{
	/// <summary>
	/// Summary description for Persistence.
	/// </summary>
	public class Persistence : NTorpedo.Auction.Persistence
	{
		public Persistence()
		{
		}

		private IContext context;

		private IContext GetContext()
		{
			if (this.context == null)
			{
				this.context = ContextManager.CreateContext();
				this.context.ExecutingSql += new ExecutingSqlEventHandler(this.ExecutingSql) ;
			}	
			return this.context;
		}

		private void ExecutingSql(object sender, SqlExecutorCancelEventArgs e)
		{
			if (!e.PostPoned)
				this.Sql += e.Sql + Environment.NewLine ;
		}

		public override void Close()
		{
			if (this.context != null)
			{
				this.context.Commit();
				this.context.Dispose();
			}
			this.context = null;
		}

		public override IBid CreateBid(string id, IAuction auction, IUser buyer, System.Double amount, System.Double maxAmount)
		{
			IContext context = GetContext();
			Bid bid = (Bid) context.CreateObject(typeof(Bid)); 
			bid.Id = id;
			bid.Auction = auction;
			bid.Buyer = buyer;
			bid.Amount = amount;
			bid.MaxAmount = maxAmount;

			return bid;
		}

		public override IAuction GetAuction(string auctionId)
		{
			IContext context = GetContext();
			IAuction auction = (IAuction) context.GetObjectById(auctionId, typeof(Auction), true); 			
			return auction;
		}

		public override IUser GetUser(string id)
		{
			IContext context = GetContext();
			IUser user = (IUser) context.GetObjectById(id, typeof(User), true); 			
			return user;
		}

		public override IList FindAllAuctions()
		{
			IContext context = GetContext();
			IQuery query = new NPathQuery("Select *, Item.*, Seller.*, Bids.* from Auction Order By Id", typeof(Auction));
			IList auctions = (IList) context.GetObjectsByQuery(query); 			
			return auctions;
		}

		public override IList FindHighBids(string auctionId)
		{
			IContext context = GetContext();
			IQuery query = new NPathQuery("select Top 1 *, Buyer.* from Bid where Auction = ? order by Amount desc", typeof(Bid));
			query.Parameters.Add(new QueryParameter(DbType.String, auctionId));
			IList bids = context.GetObjectsByQuery(query); 			
			return bids;
		}
	}
}
