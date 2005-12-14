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

namespace NTorpedo.Auction
{
	/// <summary>
	/// Summary description for BidInfo.
	/// </summary>
	[Serializable()]
	public class BidInfo
	{
		protected System.Double bidAmount;
		protected System.Double maxBidAmount;
		protected string buyer;

		public BidInfo()
		{	
		}

		public BidInfo(IBid b)
		{
			if (b!=null) 
			{
				this.bidAmount = b.Amount;
				this.maxBidAmount = b.MaxAmount;
				IUser u = b.Buyer;
				if (u!=null) 
				{
					buyer = u.Id;
				}
			}
		}

		public static IList Bids(IList bids)
		{
			IList bidInfos = new ArrayList();
			if (bids!=null) 
			{
				foreach (IBid b in bids)
					bidInfos.Add(new BidInfo(b));
			}
			return bidInfos;			
		}

		public System.Double BidAmount
		{
			get { return this.bidAmount; }
			set { this.bidAmount= value; }
		}

		public string Buyer
		{
			get { return this.buyer; }
			set { this.buyer = value; }
		}
		// This property (well getter, in java) is mysteriously lacking from the
		//	original Torpedo implementation from the Middelware company...I'm not sure
		//  if this is by mistake or serves a higher purpose whatof my brain is but to
		//	puny to comprehend, and such, so I just removed it here too....but adding this
		//  here little comment documenting why I've done this pretty strange-looking thing...
//		public System.Double MaxBidAmount
//		{
//			get { return this.maxBidAmount; }
//		}
	}
}
