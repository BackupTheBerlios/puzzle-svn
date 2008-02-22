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
using System.IO;
using System.Text;
using NTorpedo.Client.AuctionWebService;

namespace NTorpedo.Client
{
	/// <summary>
	/// Summary description for TorpedoClient.
	/// </summary>
	public class NTorpedoClient
	{
		private static string DefaultUrl = "http://localhost/NTorpedo/AuctionService.asmx";

		private string test;
		private StreamWriter output;
		private StreamWriter hitfile;
		private StringBuilder result = new StringBuilder() ;

		public NTorpedoClient(string test) : this(test, DefaultUrl)
		{
		}

		public NTorpedoClient(string test, string url)
		{
			this.test = test;
			this.auctionService = new AuctionService() ;
			this.auctionService.Url = url;

			DirectoryInfo subdir = new DirectoryInfo("results");
			subdir.Create() ;
			FileInfo fileInfo = new FileInfo(subdir.FullName + "\\" + test + ".txt");
			output = fileInfo.CreateText();
			FileInfo hitFileInfo = new FileInfo(subdir.FullName + "\\" + test + ".inc");
			hitfile = hitFileInfo.CreateText();
		}

		private void WriteLine(string str)
		{
			output.WriteLine(str);
			result.Append(str + Environment.NewLine);
 		}

		private string testResult = "";

		public string TestResult
		{
			get { return this.testResult; }
		}

		private int testScore = 0;

		public int TestScore
		{
			get { return this.testScore; }
		}

		private AuctionService auctionService  ;

		public AuctionService AuctionService
		{
			get { return this.auctionService; }
		} 

		protected void CloseResult() 
		{
			output.Close();
			hitfile.Close();
			this.testResult = result.ToString(); 
		}

		protected void PrintTestTitle() 
		{
			WriteLine("<results>" + Environment.NewLine);
			WriteLine("Executing "+test+" test" + Environment.NewLine);
		}

		protected void PrintAuction(AuctionInfo ai) 
		{
			WriteLine("AuctionID="+ai.AuctionID);
			WriteLine("Item "+ai.ItemName);
			WriteLine("ItemID="+ai.ItemID);
			WriteLine("Description "+ai.Description);
			WriteLine("Graphic file name="+ai.ItemGraphicFilename);
			WriteLine("Lowest price="+ai.LowPrice.ToString() );
			WriteLine("Seller's name is "+ai.SellerName);
			PrintBids(ai.Bids);
		}

		protected void PrintSQL(String sql) 
		{
			if (sql == null)
				sql = "";
			WriteLine("</results>");
			WriteLine("<sql>");
			WriteLine(sql);
			int hits=0;
			if (sql.Length > 0) 
			{
				string[] arr = sql.Split(Environment.NewLine.ToCharArray());
				hits = arr.Length - 1;
				this.testScore = hits;
			}
			WriteLine("</sql>");
			hitfile.WriteLine(hits);
		}
		protected void PrintAuctions(IList auctions) 
		{
			foreach (AuctionInfo ai in auctions)
			{
				WriteLine(ai.ItemName);				
			}
		}
		protected void PrintBids(IList bids) 
		{
			foreach (BidInfo bid in bids)
			{
				PrintBid(bid);
			}
		}
		protected void PrintBid(BidInfo bi) 
		{
			WriteLine(bi.Buyer+" bid "+bi.BidAmount.ToString() );
		}

	}
}
