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
using System.Xml.Serialization;

namespace NTorpedo.Auction
{
	/// <summary>
	/// Summary description for AuctionInfo.
	/// </summary>
	[Serializable()]
	public class AuctionInfo
	{
		protected string description;
		protected System.Double lowPrice;
		protected IList bids;
		protected string sellerName;
		protected string itemName;
		protected string itemGraphicFilename;
		protected string itemID;
		protected string auctionID;
		protected string sql = "";

		public AuctionInfo()
		{
		}

		public AuctionInfo(IAuction auction, bool partial)
		{
			this.lowPrice = auction.LowPrice;
			this.auctionID = auction.Id;
			IItem i = auction.Item;
			if (i != null)
				this.itemName = i.ItemName;
			if (partial)
			{
				//not used
				this.bids = new ArrayList();
				this.itemGraphicFilename="not needed";
				this.sellerName = "not needed";
				this.itemID = "not needed";
				this.description = "not needed"; 
			}
			else
			{
				this.bids = BidInfo.Bids(auction.Bids);
				IUser u = auction.Seller;
				if (u!=null) 
				{
					sellerName = u.Id;
				}
				if (i!=null) 
				{
					description = i.Description;
					itemID = i.Id;
					itemGraphicFilename = i.GraphicFilename;
				}
				
			}
		}

		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		public System.Double LowPrice
		{
			get { return this.lowPrice; }
			set { this.lowPrice = value; }
		}

		[XmlArrayItem(typeof(BidInfo))]
		public IList Bids
		{
			get { return this.bids; }
			set { this.bids = value; }
		}

		public string SellerName
		{
			get { return this.sellerName; }
			set { this.sellerName = value; }
		}

		public string ItemName
		{
			get { return this.itemName; }
			set { this.itemName = value; }
		}

		public string ItemGraphicFilename
		{
			get { return this.itemGraphicFilename; }
			set { this.itemGraphicFilename = value; }
		}

		public string ItemID
		{
			get { return this.itemID; }
			set { this.itemID = value; }
		}

		public string AuctionID
		{
			get { return this.auctionID; }
			set { this.auctionID = value; }
		}

		public string Sql
		{
			get { return this.sql; }
			set { this.sql = value; }
		}

	
	}
}
